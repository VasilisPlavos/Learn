using System.Net;
using Codelux.NetCore.Common.Models;

namespace Example.Users.ManagementService.Common
{
    public static class ServiceErrors
    {
        public static ServiceErrorException CannotCreateUserException =
            new ServiceErrorException(ServiceConstants.ServiceName, 0, HttpStatusCode.InternalServerError,
                "Unable to create user. Please contact an administrator.");

        public static ServiceErrorException InvalidCredentialsException =
            new ServiceErrorException(ServiceConstants.ServiceName, 0, HttpStatusCode.NotFound,
                "Username or password is not correct.");
    }
}
