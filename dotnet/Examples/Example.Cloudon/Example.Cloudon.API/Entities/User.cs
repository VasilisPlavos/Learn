using System.ComponentModel.DataAnnotations;

namespace Example.Cloudon.API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}