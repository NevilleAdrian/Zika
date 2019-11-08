using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zika.Models
{
    public class Testimonial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestimonialId { get; set; }
        [Required]
        [Display(Name = "Client name")]
        public string Name { get; set; }
        [Display(Name = "Client response")]
        public string Response { get; set; }
    }
}
