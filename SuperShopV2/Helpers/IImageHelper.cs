using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SuperShopV2.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
