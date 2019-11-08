using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Zika.Enum;

namespace Zika.Models
{
    public class Freight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FreightId { get; set; }

        [Required(ErrorMessage = "Please select where you are shipping from")]
        [Display(Name = "From")]
        public string FreightFrom { get; set; }
        [Display(Name = "To")]

        [Required(ErrorMessage = "Please indicate address to deliver to")]
        public string FreightTo { get; set; }
        [Display(Name = "Has paid")]
        public bool HasPaid { get; set; }
        public HaulageStatus Status { get; set; }
        public double Amount { get; set; }
        public double Mass { get; set; }

        [Display(Name = "Freight Summary")]
        public string FreightSummary { get; set; }

        [Display(Name = "Date Due")]
        public DateTime DateDue { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}
