using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Entities
{
    public class ZipCode
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
    }
}
