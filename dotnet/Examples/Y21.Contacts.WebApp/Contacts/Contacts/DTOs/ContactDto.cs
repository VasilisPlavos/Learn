using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Entities;

namespace Contacts.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LocalPhone { get; set; }
        public string MobilePhone { get; set; }
        public ZipCode ZipCode { get; set; }
    }
}
