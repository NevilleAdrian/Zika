using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zika.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailId { get; set; }

        [Display(Name = "Users' emails")]
        public string UserEmails { get; set; }
    }
}
