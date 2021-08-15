using Funq;
using System.Reflection;
using Example.Users.ManagementService.Common;
using ServiceStack;

namespace Example.Users.ManagementService.Web
{
    public class AppHost : AppHostBase
    {
        private AppConfigurator _appConfigurator;
        private readonly string _environment;

        public AppHost(string environment, params Assembly[] assembliesWithServices) : base(ServiceConstants.ServiceName, typeof(AppConfigurator).Assembly)
        {
            _environment = environment;
        }

        public override void Configure(Container container)
        {
            _appConfigurator = new AppConfigurator(_environment, this);
        }
    }
}
