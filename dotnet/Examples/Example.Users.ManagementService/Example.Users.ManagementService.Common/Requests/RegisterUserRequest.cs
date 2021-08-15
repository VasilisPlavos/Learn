using Codelux.NetCore.Common.Requests;

namespace Example.Users.ManagementService.Common.Requests
{
    public class RegisterUserRequest : Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
