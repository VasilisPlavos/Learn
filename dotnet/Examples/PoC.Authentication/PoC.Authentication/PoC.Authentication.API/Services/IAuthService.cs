using Microsoft.IdentityModel.Tokens;
using PoC.Authentication.API.Contracts;
using PoC.Authentication.API.Data;
using PoC.Authentication.API.Entities;
using PoC.Authentication.API.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace PoC.Authentication.API.Services;

public interface IAuthService
{
    Task<AccessOrRefreshResponse> AccessAuthenticatedOrAnonymousUserAsync(AccessRequest request);
    Task<bool> CreateUserAsync(RegisterRequest request);
    Task<AccessOrRefreshResponse> RefreshAuthorizedUserAsync(HttpRequest request, string refreshToken);
    Task<bool> RevokeUserTokenAsync(HttpRequest request, string refreshToken);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    public AuthService(ApplicationDbContext db)
    {
        _db = db;
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
            if (request.JwtToMerge != null)
            {
                var sourceUserId = UtilHelper.GetUserId(request.JwtToMerge);
                if (sourceUserId != null)
                {
                    await MoveOwnershipAsync(sourceUserId, user.Id);
                }
            }
        }

        return await PrepareAccessResponseAsync(user.Id);
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.Jwt.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var now = DateTime.UtcNow;
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: now.AddHours(1),
            notBefore: now,
            signingCredentials: creds
        );

        return token;
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
    {
        // TODO: exclude +, ?, # and whatelse?
        const int tokenLength = 32;
        var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomBytes = new byte[tokenLength];
        randomNumberGenerator.GetBytes(randomBytes);
        var refreshTokenValue = Convert.ToBase64String(randomBytes);

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