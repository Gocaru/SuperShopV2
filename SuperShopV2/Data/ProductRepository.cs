using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using System.Linq;

namespace SuperShopV2.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User);
        }
    }
}
