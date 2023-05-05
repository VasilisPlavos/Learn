using Microsoft.EntityFrameworkCore;
using PoC.Authentication.API.Data;
using PoC.Authentication.API.Entities;
using System.Security.Cryptography;
using PoC.Authentication.API.Helpers;

namespace PoC.Authentication.API.Services;

public interface IProjectsService
{
    Task<Project> CreateUserProjectAsync(HttpRequest request);
    Task<bool> ClaimOwnershipAsync(HttpRequest request, string sourceJwt);
    Task<List<Project>> GetUserProjectsAsync(HttpRequest request);
}

public class ProjectsService : IProjectsService
{
    private readonly ApplicationDbContext _db;
    private readonly IAuthService _authService;
    public ProjectsService(ApplicationDbContext db, IAuthService authService)
    {
        _db = db;
        _authService = authService;
    }
    public async Task<Project> CreateUserProjectAsync(HttpRequest request)
    {
        var userId = UtilHelper.GetUserId(request);
        var project = new Project()
        {
            OwnerId = userId,
            Title = $"Project {RandomNumberGenerator.GetInt32(1, 10000)}"
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return project;
    }

    public async Task<bool> ClaimOwnershipAsync(HttpRequest request, string sourceJwt)
    {
        var sourceUserId = UtilHelper.GetUserId(sourceJwt);
        var destinationId = UtilHelper.GetUserId(request);
        await _authService.MoveOwnershipAsync(sourceUserId, destinationId);
        return true;
    }
    public async Task<List<Project>> GetUserProjectsAsync(HttpRequest request)
    {
        var userId = UtilHelper.GetUserId(request);
        return await _db.Projects.Where(x => x.OwnerId == userId).ToListAsync();

    }
}