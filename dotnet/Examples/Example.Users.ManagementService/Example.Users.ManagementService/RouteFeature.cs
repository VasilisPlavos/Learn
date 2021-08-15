using ServiceStack;
using Example.Users.ManagementService.Common.Requests;

namespace Example.Users.ManagementService
{
    public class RouteFeature : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.Routes.Add<RegisterUserRequest>("/api/users", "POST");
            appHost.Routes.Add<AuthenticateUserRequest>("/api/users", "GET");
        }
    }
}
