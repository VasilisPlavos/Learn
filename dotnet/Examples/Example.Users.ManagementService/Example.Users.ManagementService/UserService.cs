using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Codelux.NetCore.Common.Extensions;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Common.Responses;
using Example.Users.ManagementService.Executors.AuthenticateUserExecutor;
using Example.Users.ManagementService.Executors.RegisterUserExecutor;
using ServiceStack;

namespace Example.Users.ManagementService
{
    public class UserService : Service
    {
        private readonly IRegisterUserExecutor _registerUserExecutor;
        private readonly IAuthenticateUserExecutor _authenticateUserExecutor;

        public UserService(IRegisterUserExecutor registerUserExecutor, IAuthenticateUserExecutor authenticateUserExecutor)
        {
            registerUserExecutor.Guard(nameof(registerUserExecutor));
            authenticateUserExecutor.Guard(nameof(authenticateUserExecutor));

            _registerUserExecutor = registerUserExecutor;
            _authenticateUserExecutor = authenticateUserExecutor;
        }

        public Task<RegisterUserResponse> Post(RegisterUserRequest request)
        {
            return _registerUserExecutor.ExecuteAsync(request);
        }

        public Task<AuthenticateUserResponse> Get(AuthenticateUserRequest request)
        {
            return _authenticateUserExecutor.ExecuteAsync(request);
        }
    }
}
