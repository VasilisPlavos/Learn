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
    Task<bool> CreateUserAsync(RegisterRequest request);
    Task<AccessOrRefreshResponse> RefreshAuthorizedUserAsync(HttpRequest request, string refreshToken);
    Task<bool> RevokeUserTokenAsync(HttpRequest request, string refreshToken);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly string _secret;

    public AuthService(ApplicationDbContext db, IConfiguration configuration)
    {
        _db = db;
        _secret = configuration["JWT:Secret"];
    }

    public async Task<AccessOrRefreshResponse?> AccessAuthenticatedOrAnonymousUserAsync(AccessRequest request)
    {
        User user;
        if (request.Anonymous)
        {
            user = await CreateUserAsync(null, "Anonymous");
        }
        else
        {
            if (request.Email == null) return null;
            user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null) return null;
            if (request.SourceJwtToMove != null)
            {
                var sourceUserId = UtilHelper.GetUserId(request.SourceJwtToMove);
                await MoveOwnershipAsync(sourceUserId, user.Id);
            }
        }

        return await PrepareAccessResponseAsync(user.Id);
    }

    public async Task<bool> ClaimOwnershipAsync(HttpRequest request, string sourceJwt)
    {
        var sourceUserId = UtilHelper.GetUserId(sourceJwt);
        var destinationId = UtilHelper.GetUserId(request);
        await MoveOwnershipAsync(sourceUserId, destinationId);
        return true;
    }

    public async Task<bool> CreateUserAsync(RegisterRequest request)
    {
        var user = await CreateUserAsync(request.Email, "Authenticated");
        if (request.JwtToMerge != null)
        {
            var userIdAsAnonymous = UtilHelper.GetUserId(request.JwtToMerge);
            await MoveOwnershipAsync(userIdAsAnonymous, user.Id);
        }

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
            //new(ClaimTypes.Email, anonymousOrUserEmail ?? ""),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: now.AddHours(1), //.AddMinutes(10),
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
            ExpirationDate = DateTime.UtcNow.AddDays(30),
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
        var key = Encoding.UTF8.GetBytes(_secret);
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
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            AccessToken_Exp = accessToken.ValidTo.Ticks.ToString(),
            RefreshToken = refreshToken.Value,
            RefreshToken_Exp = refreshToken.ExpirationDate.Ticks.ToString()
        };
    }

    public async Task<AccessOrRefreshResponse> RefreshAuthorizedUserAsync(HttpRequest request, string refreshTokenValue)
    {
        var userId = UtilHelper.GetUserId(request);
        var success = await RevokeUserTokenAsync(userId, refreshTokenValue);
        if (!success) return null;
        return await PrepareAccessResponseAsync(userId);
    }

    public async Task<bool> RevokeUserTokenAsync(HttpRequest request, string refreshToken)
    {
        var userId = UtilHelper.GetUserId(request);
        return await RevokeUserTokenAsync(userId, refreshToken);
    }

    private async Task<bool> RevokeUserTokenAsync(Guid userId, string refreshTokenValue)
    {

        var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync
        (x =>
            x.Value == refreshTokenValue
            && x.ExpirationDate > DateTime.UtcNow
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