using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SuperShopV2.Data.Entities;


namespace SuperShopV2.Models
{
    /// <summary>
    /// ViewModel que representa os dados de um produto com suporte adicional para envio de imagem através de formulário.
    /// Herdada da entidade <see cref="Product"/> para reutilização das propriedades base.
    /// </summary>
    public class ProductViewModel : Product
    {
        [Display(Name ="Image")]
        public IFormFile ImageFile { get; set; }
    }
}
