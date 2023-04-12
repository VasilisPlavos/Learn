using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerAPI.Models
{
    public class ContactAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public int AddressZIPCode { get; set; }
    }
}
