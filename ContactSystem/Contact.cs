//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter lst name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter phone number")]
        [Display(Name = "Phone Number")]
        public long PhoneNo { get; set; }

        [Required(ErrorMessage = "Enter valid email address")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter Status")]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
