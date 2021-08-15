using ServiceStack;
using Codelux.NetCore.ServiceStack.Utilities;
using Codelux.NetCore.Utilities;
using Codelux.NetCore.Utilities.Crypto;

namespace Example.Users.ManagementService.Dependencies
{
    public class UtilitiesModule : DependencyModuleBase
    {
        public UtilitiesModule(ServiceStackHost appHost) : base(appHost)
        {
        }

        public override void RegisterDependencies()
        {
            AppHost.Container.RegisterAutoWiredAs<ClockService, IClockService>();
            AppHost.Container.RegisterAutoWiredAs<Md5PasswordEncryptor, IPasswordEncryptor>();
        }
    }

}
