using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SuperShopV2.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);   //Recebe um ficheiro de um formulário

        Task<Guid> UploadBlobAsync(byte[] file, string containerName);   //Recebe um array de bytes (por exemplo, quando recebo uma imagem de um telemóvel)

        Task<Guid> UploadBlobAsync(string image, string containerName);   //Recebe um endereço de uma imagem
    }
}
