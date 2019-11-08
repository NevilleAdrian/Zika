using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zika.Models
{
    public class FAQ
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FAQId { get; set; }
        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }
        [Display(Name = "Answer")]
        public string Answer { get; set; }
        
    }
}
