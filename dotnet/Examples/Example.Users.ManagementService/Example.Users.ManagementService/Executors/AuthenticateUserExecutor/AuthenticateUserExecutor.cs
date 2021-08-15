using System.Threading;
using System.Threading.Tasks;
using Codelux.NetCore.Common.Extensions;
using Codelux.NetCore.Executors;
using Codelux.NetCore.Utilities.Crypto;
using Example.Users.ManagementService.Common;
using Example.Users.ManagementService.Common.Models;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Common.Responses;
using Example.Users.ManagementService.Core.DataAccess;

namespace Example.Users.ManagementService.Executors.AuthenticateUserExecutor
{
    public class AuthenticateUserExecutor : ExecutorBase<AuthenticateUserRequest, AuthenticateUserResponse>, IAuthenticateUserExecutor
    {
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IUserRepository _userRepository;

        public AuthenticateUserExecutor(IPasswordEncryptor passwordEncryptor, IUserRepository userRepository)
        {
            passwordEncryptor.Guard(nameof(passwordEncryptor));
            userRepository.Guard(nameof(userRepository));

            _passwordEncryptor = passwordEncryptor;
            _userRepository = userRepository;
        }


        protected override async Task<AuthenticateUserResponse> OnExecuteAsync(AuthenticateUserRequest tin, CancellationToken token = new CancellationToken())
        {
            string hashedPassword = _passwordEncryptor.Encrypt(tin.Password);

            User user = await _userRepository.GetUserByCredentialsAsync(tin.Username, hashedPassword, token)
                .ConfigureAwait(false);

            if (user == null) throw ServiceErrors.InvalidCredentialsException;

            return new AuthenticateUserResponse()
            {
                UserId = user.Id
            };
        }
    }
}
