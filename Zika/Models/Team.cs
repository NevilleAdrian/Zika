using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zika.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Person's Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Facebook URL")]
        [DataType(DataType.Url)]
        public string Facebook { get; set; }
        [Display(Name = "Instagram URL")]
        [DataType(DataType.Url)]
        public string Instagram { get; set; }
        [DataType(DataType.Url)]
        [Display(Name = "Twitter URL")]
        public string Twitter { get; set; }
        [DataType(DataType.Url)]
        [Display(Name = "Skype URL")]
        public string Skype { get; set; }
    }
}
