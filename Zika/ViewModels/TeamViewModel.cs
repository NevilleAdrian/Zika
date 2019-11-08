using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Zika.Models;

namespace Zika.ViewModels
{
    public class TeamViewModel : Team
    {
        [Display(Name = "Member image")]
        public IFormFile File { get; set; }
    }
}
