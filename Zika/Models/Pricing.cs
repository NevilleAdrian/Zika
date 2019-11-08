using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zika.Models
{
    public class Pricing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricingId { get; set; }

        [Required]
        [Display(Name = "Pricing details")]
        public string PricingInformation { get; set; }
    }
}
