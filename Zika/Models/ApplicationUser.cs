using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zika.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Address 1")]
        [DataType(DataType.Text)]
        public string AddressOne { get; set; }

        [Display(Name = "Address 2")]
        [DataType(DataType.Text)]
        public string AddressTwo { get; set; }
        [DataType(DataType.Text)]
        public string City { get; set; }
        [DataType(DataType.Text)]
        public string State { get; set; }
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [DataType(DataType.Text)]
        public string Country { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public bool IAgreeToTerms { get; set; }
    }
}
