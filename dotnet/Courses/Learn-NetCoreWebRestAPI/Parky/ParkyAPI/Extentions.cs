using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI
{
    public static class Extentions
    {
        public static UserDto AsDto(this User user)
        {
            if (user == null) return null;
            return new UserDto { username = user.Username, sex = "Female" };
        }
    }
}
