using System;
using System.ComponentModel.DataAnnotations;
namespace Zika.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        [DataType(DataType.Text)]
        public string AddressOne { get; set; }

        [Display(Name = "Address 2")]
        [DataType(DataType.Text)]
        public string AddressTwo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string State { get; set; }
        

        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Country { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "I agree to terms")]
        public bool IAgreeToTerms { get; set; }
    }
}
