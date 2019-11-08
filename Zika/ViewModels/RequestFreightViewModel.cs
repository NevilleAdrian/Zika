using System.ComponentModel.DataAnnotations;

namespace Zika.ViewModels
{
    public class RequestFreightViewModel
    {
        [Required(ErrorMessage = "Please use a valid email for this freight")]
        [Display(Name = "Email address for the freight")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please use a valid freight ID")]
        [Display(Name = "Freight ID for the freight")]
        public int FreightId { get; set; }
    }
}
