using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zika.Models
{
    public class Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExchangeId { get; set; }
        [Required]
        [Display(Name = "Currency Name")]
        public string Name { get; set; }
        [Display(Name = "Exchange Rate in Naira")]
        public double RateInNaira { get; set; }
        [Display(Name = "Exchange Rate in Ghana Cedis")]
        public double RateInCedis { get; set; }
    }
}
