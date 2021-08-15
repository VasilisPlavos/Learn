using ServiceStack;
using Codelux.NetCore.ServiceStack.Utilities;
using Example.Users.ManagementService.Executors.AuthenticateUserExecutor;
using Example.Users.ManagementService.Executors.RegisterUserExecutor;

namespace Example.Users.ManagementService.Dependencies
{
    public class ExecutorsModule : DependencyModuleBase
    {
        public ExecutorsModule(ServiceStackHost appHost) : base(appHost)
        {
        }

        public override void RegisterDependencies()
        {
            AppHost.Container.RegisterAutoWiredAs<RegisterUserExecutor, IRegisterUserExecutor>();
            AppHost.Container.RegisterAutoWiredAs<AuthenticateUserExecutor, IAuthenticateUserExecutor>();
        }
    }
}
