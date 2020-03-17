using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactSystem.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNo { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

    }
}