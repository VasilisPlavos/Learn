using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parky2API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Passwrod { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}
