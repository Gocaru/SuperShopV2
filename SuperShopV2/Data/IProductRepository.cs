using SuperShopV2.Data.Entities;
using System.Linq;

namespace SuperShopV2.Data
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        public IQueryable GetAllWithUsers();
    }
}
