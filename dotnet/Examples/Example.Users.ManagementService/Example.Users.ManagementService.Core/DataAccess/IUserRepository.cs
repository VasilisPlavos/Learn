using System;
using System.Threading;
using System.Threading.Tasks;
using Example.Users.ManagementService.Common.Models;

namespace Example.Users.ManagementService.Core.DataAccess
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(User model, CancellationToken token = default);
        Task<User> GetUserByCredentialsAsync(string username, string password, CancellationToken token = default);
        Task<bool> UpdateUserAsync(Guid userId, User model, CancellationToken token = default);
    }
}