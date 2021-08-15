using Codelux.NetCore.ServiceStack.OrmLite;
using Example.Users.ManagementService.Common.Models;

namespace Example.Users.ManagementService.Core.DataAccess.Mappings
{
    public class UserMapping : OrmLiteMapping<User>
    {
        public UserMapping()
        {
            MapToTable("users");
            MapToColumn(x => x.Id, "id");
            MapToColumn(x => x.Username, "username");
            MapToColumn(x => x.Password, "password");
            MapToColumn(x => x.Email, "email");
            MapToColumn(x => x.FirstName, "first_name");
            MapToColumn(x => x.LastName, "last_name");
            MapToColumn(x => x.CreatedAt, "created_at");
        }
    }
}
