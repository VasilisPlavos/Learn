using Microsoft.EntityFrameworkCore;
using PoC.Authentication.API.Data;
using PoC.Authentication.API.Entities;
using System.Security.Cryptography;
using PoC.Authentication.API.Helpers;

namespace PoC.Authentication.API.Services;

public interface IProjectsService
{
    Task<Project> CreateUserProjectAsync(HttpRequest request);
    Task<List<Project>> GetUserProjectsAsync(HttpRequest request);
}

public class ProjectsService : IProjectsService
{
    private readonly ApplicationDbContext _db;
    public ProjectsService(ApplicationDbContext db) { _db = db; }
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
    public async Task<List<Project>> GetUserProjectsAsync(HttpRequest request)
    {
        var userId = UtilHelper.GetUserId(request);
        return await _db.Projects.Where(x => x.OwnerId == userId).ToListAsync();
    }
}