using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LocalPhone { get; set; }
        public string MobilePhone { get; set; }
        public int ZipCodeId { get; set; }

    }
}
