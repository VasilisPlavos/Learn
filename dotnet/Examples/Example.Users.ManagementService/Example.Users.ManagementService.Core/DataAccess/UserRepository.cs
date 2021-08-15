using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Codelux.NetCore.Common.Extensions;
using Example.Users.ManagementService.Common.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Example.Users.ManagementService.Core.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            dbConnectionFactory.Guard(nameof(dbConnectionFactory));
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> CreateUserAsync(User model, CancellationToken token = default)
        {
            using IDbConnection db = _dbConnectionFactory.OpenDbConnection();
            return await db.InsertAsync<User>(model, false, token).ConfigureAwait(false) > 0;
        }

        public async Task<User> GetUserByCredentialsAsync(string username, string password, CancellationToken token = default)
        {
            using IDbConnection db = _dbConnectionFactory.OpenDbConnection();
            return await db.SingleAsync<User>(x => x.Username == username && x.Password == password, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateUserAsync(Guid userId, User model, CancellationToken token = default)
        {
            using IDbConnection db = _dbConnectionFactory.OpenDbConnection();
            return await db.UpdateAsync<User>(model, x => x.Id == userId, null, token).ConfigureAwait(false) > 0;
        }
    }
}
