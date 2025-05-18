using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace SuperShopV2.Helpers
{

    public class BlobHelper : IBlobHelper
    {
        private readonly CloudBlobClient _blobClient;

        public BlobHelper(IConfiguration configuration)
        {
            string keys = configuration["Blob:ConnectionString"];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(keys);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<Guid> UploadBlobAsync(IFormFile file, string containerName)
        {
            Stream stream = file.OpenReadStream();
            return await UploadStreamAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(byte[] file, string containerName)
        {
            MemoryStream stream = new MemoryStream(file);
            return await UploadStreamAsync(stream, containerName);
        }

        public async Task<Guid> UploadBlobAsync(string image, string containerName)
        {
            Stream stream = File.OpenRead(image);
            return await UploadStreamAsync(stream, containerName);
        }

        /// <summary>
        /// Carrega um fluxo de dados (por exemplo, uma imagem) para o Azure Blob Storage,
        /// atribuindo-lhe um nome único baseado num Guid.
        /// </summary>
        /// <param name="stream">O fluxo de dados a enviar para o blob (por exemplo, um ficheiro ou imagem).</param>
        /// <param name="containerName">O nome do contentor onde o ficheiro será armazenado.</param>
        /// <returns>Retorna o Guid gerado para identificar o ficheiro carregado.</returns>
        private async Task<Guid> UploadStreamAsync(Stream stream, string containerName) //Para não haver ficheiros com nomes repetidos
        {
            Guid name = Guid.NewGuid();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"{name}");
            await blockBlob.UploadFromStreamAsync(stream);
            return name;
        }
    }
}
