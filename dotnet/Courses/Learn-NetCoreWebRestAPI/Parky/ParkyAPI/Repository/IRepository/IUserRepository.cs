using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Register(string username, string password);
        User Authenticate(string username, string password);
    }
}
