using Microsoft.AspNetCore.Mvc.Rendering;
using SuperShopV2.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SuperShopV2.Data
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboProducts();  //Dá os produtos já prontos para colocar na combobox

    }
}
