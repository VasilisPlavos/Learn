using Codelux.NetCore.Executors;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Common.Responses;

namespace Example.Users.ManagementService.Executors.AuthenticateUserExecutor
{
    public interface IAuthenticateUserExecutor: IExecutor<AuthenticateUserRequest, AuthenticateUserResponse> { }
}
