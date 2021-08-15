using Codelux.NetCore.Executors;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Common.Responses;

namespace Example.Users.ManagementService.Executors.RegisterUserExecutor
{
    public interface IRegisterUserExecutor : IExecutor<RegisterUserRequest, RegisterUserResponse> { }
}
