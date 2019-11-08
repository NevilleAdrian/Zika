
using Microsoft.AspNetCore.Http;

namespace Zika.Services
{
    public interface IImageService
    {
        string CreateImage(IFormFile file);
        string EditImage(IFormFile file, string imageUrl);
        void DeleteImage(string ImageUrl);
    }
}
