using Codelux.NetCore.Common.Requests;

namespace Example.Users.ManagementService.Common.Requests
{
    public class AuthenticateUserRequest : Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
