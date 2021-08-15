using System;
using System.Collections.Generic;
using System.Text;
using Codelux.NetCore.ServiceStack;
using Codelux.NetCore.ServiceStack.OrmLite;
using Example.Users.ManagementService.Common;
using Example.Users.ManagementService.Dependencies;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Validation;

namespace Example.Users.ManagementService
{
    public class AppConfigurator : CoreAppConfiguratorBase
    {
        private string _environment;

        public AppConfigurator(string environment, ServiceStackHost appHost) : base(ServiceConstants.ServiceName, appHost)
        {
            _environment = environment;

            appHost.Plugins.Add(new RouteFeature());
            appHost.Plugins.Add(new CorsFeature());
            appHost.Plugins.Add(new OpenApiFeature());
            appHost.Plugins.Add(new OrmLiteMappingFeature());
            appHost.Plugins.Add(new ValidationFeature());

            new ExecutorsModule(appHost).RegisterDependencies();
            new UtilitiesModule(appHost).RegisterDependencies();
            new MappersModule(appHost).RegisterDependencies();
            new DatabaseModule(appHost).RegisterDependencies();

            appHost.Container.RegisterValidators(typeof(AppConfigurator).Assembly);
        }
    }
}
