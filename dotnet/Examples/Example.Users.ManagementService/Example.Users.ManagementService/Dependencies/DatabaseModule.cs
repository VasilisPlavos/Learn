using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Codelux.NetCore.ServiceStack.Utilities;
using Example.Users.ManagementService.Core.DataAccess;


namespace Example.Users.ManagementService.Dependencies
{
    public class DatabaseModule : DependencyModuleBase
    {
        public DatabaseModule(ServiceStackHost appHost) : base(appHost)
        {
        }

        public override void RegisterDependencies()
        {
            IDbConnectionFactory factory = new OrmLiteConnectionFactory("Server=localhost;Database=example;Uid=root;Pwd=;", MySql55Dialect.Provider);

            AppHost.Container.Register<IDbConnectionFactory>(factory);
            AppHost.Container.RegisterAutoWiredAs<UserRepository, IUserRepository>();
        }
    }
}
