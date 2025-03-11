using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Data;
using PoC.Authentication.API.Entities;
using PoC.Authentication.API.Helpers;

namespace PoC.Authentication.API.Services;

public interface IAuthService
{
    Task<AccessOrRefreshResponse> AccessAuthenticatedOrAnonymousUserAsync(AccessRequest request);
    Task<bool> ClaimOwnershipAsync(HttpRequest request, string sourceJwt);
    Task<bool> RegisterUserAsync(RegisterRequest request);
    Task<AccessOrRefreshResponse> RefreshAuthorizedUserAsync(IHeaderDictionary headers, string refreshToken);
    Task<bool> RevokeUserTokenAsync(IHeaderDictionary headers, string refreshToken);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly BearerSettings _bearerSettings;

    public AuthService(ApplicationDbContext db, BearerSettings bearerSettings)
    {
        _bearerSettings = bearerSettings;
        _db = db;
    }


    public async Task<AccessOrRefreshResponse?> AccessAuthenticatedOrAnonymousUserAsync(AccessRequest request)
    {
        User user;
        if (request.Anonymous) user = await CreateUserAsync(null, "Anonymous");
        else
        {
            if (request.Email == null) return null;
            user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null) return null;
        }

        var sourceJwt = UtilHelper.GetToken(request.SourceJwtToMove);
        if (sourceJwt == null) return await PrepareAccessResponseAsync(user.Id);

        var sourceUserId = UtilHelper.GetUserId(sourceJwt);
        await MoveOwnershipAsync(sourceUserId, user.Id);
        return await PrepareAccessResponseAsync(user.Id);
    }

    public async Task<bool> ClaimOwnershipAsync(HttpRequest request, string sourceJwt)
    {
        var jwtSecurityToken = UtilHelper.GetToken(sourceJwt);
        var sourceUserId = UtilHelper.GetUserId(jwtSecurityToken);
        var destinationId = UtilHelper.GetUserId(request.Headers);
        await MoveOwnershipAsync(sourceUserId, destinationId);
        return true;
    }

    private async Task<User> CreateUserAsync(string? email, string role)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Role = role
        };

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }

    private JwtSecurityToken GenerateAccessToken(Guid userId)
    {
        var claims = new List<Claim>
        {
            new("userId", userId.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_bearerSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: now.AddMinutes(15),
            notBefore: now,
            signingCredentials: creds
        );

        return token;
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
    {
        var refreshTokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)).Replace("+", "-")
            .Replace("/", "_").Replace("=", "");
        var refreshToken = new RefreshToken()
        {
            ExpirationDate = DateTimeOffset.UtcNow.AddDays(30).ToUnixTimeSeconds(),
            IsActive = true,
            UserId = userId,
            Value = refreshTokenValue
        };

        await _db.RefreshTokens.AddAsync(refreshToken);
        await _db.SaveChangesAsync();
        return refreshToken;
    }

    // TODO:
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string jwt)
    {
        var key = Encoding.UTF8.GetBytes(_bearerSettings.Secret);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = (JwtSecurityToken)securityToken ?? throw new SecurityTokenException("Invalid token");
        if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private async Task MoveOwnershipAsync(Guid sourceUserId, Guid destinationUserId)
    {
	    var projects = await _db.Projects.Where(x => x.OwnerId == sourceUserId).ToListAsync();
        foreach (var project in projects)
        {
            project.OwnerId = destinationUserId;
           _db.Projects.Update(project);
        }

        await _db.SaveChangesAsync();
    }

    private async Task<AccessOrRefreshResponse> PrepareAccessResponseAsync(Guid userId)
    {
        var accessToken = GenerateAccessToken(userId);
        var refreshToken = await GenerateRefreshTokenAsync(userId);
        
        return new AccessOrRefreshResponse
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(accessToken),
            access_token_exp = (long)accessToken.ValidTo.Subtract(DateTime.UnixEpoch).TotalSeconds,
            refresh_token = refreshToken.Value,
            refresh_token_exp = refreshToken.ExpirationDate
        };
    }

    public async Task<bool> RegisterUserAsync(RegisterRequest request)
    {
        var user = await CreateUserAsync(request.Email, "Authenticated");
        if (request.JwtToMerge == null) return true;

        var jwtSecurityToken = UtilHelper.GetToken(request.JwtToMerge);
        if (jwtSecurityToken == null) return true;

        var sourceUserId = UtilHelper.GetUserId(jwtSecurityToken);
        await MoveOwnershipAsync(sourceUserId, user.Id);
        return true;
    }

    public async Task<AccessOrRefreshResponse> RefreshAuthorizedUserAsync(IHeaderDictionary headers, string refreshTokenValue)
    {
        var userId = UtilHelper.GetUserId(headers);
        var success = await RevokeUserTokenAsync(userId, refreshTokenValue);
        if (!success) return null;
        return await PrepareAccessResponseAsync(userId);
    }

    public async Task<bool> RevokeUserTokenAsync(IHeaderDictionary headers, string refreshToken)
    {
        var userId = UtilHelper.GetUserId(headers);
        return await RevokeUserTokenAsync(userId, refreshToken);
    }

    private async Task<bool> RevokeUserTokenAsync(Guid userId, string refreshTokenValue)
    {
        var nowToUnixTimeSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync
        (x =>
            x.Value == refreshTokenValue 
            && x.ExpirationDate > nowToUnixTimeSeconds
            && x.IsActive 
            && x.UserId == userId
         );

        if (refreshToken == null) return false;

        refreshToken.IsActive = false;
        _db.Update(refreshToken);
        await _db.SaveChangesAsync();
        return true;
    }
}