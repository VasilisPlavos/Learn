using ServiceStack;
using Codelux.NetCore.Mappers;
using Codelux.NetCore.ServiceStack.Utilities;
using Example.Users.ManagementService.Common.Models;
using Example.Users.ManagementService.Common.Requests;
using Example.Users.ManagementService.Core.Mappers;

namespace Example.Users.ManagementService.Dependencies
{
    public class MappersModule : DependencyModuleBase
    {
        public MappersModule(ServiceStackHost appHost) : base(appHost)
        {
        }

        public override void RegisterDependencies()
        {
            AppHost.Container.RegisterAutoWiredAs<RegisterUserRequestToUserMapper, IMapper<RegisterUserRequest, User>>();
        }
    }
}
