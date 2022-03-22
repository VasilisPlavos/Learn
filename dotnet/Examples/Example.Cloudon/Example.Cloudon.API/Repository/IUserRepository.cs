using System.Threading.Tasks;
using Example.Cloudon.API.Databases;
using Example.Cloudon.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Cloudon.API.Repository
{
    public interface IUserRepository
    {
        Task<bool> AnyAsync(string username);
        Task<bool> AnyAsync(string username, string hashedPassword);
        Task<bool> AddAsync(string username, string hashedPassword);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AnyAsync(string username)
        {
            return await _db.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<bool> AnyAsync(string username, string hashedPassword)
        {
            return await _db.Users.AnyAsync(x => x.Username == username && x.HashedPassword == hashedPassword);
        }

        public async Task<bool> AddAsync(string username, string hashedPassword)
        {
            var user = new User()
            {
                Username = username, 
                HashedPassword = hashedPassword
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}