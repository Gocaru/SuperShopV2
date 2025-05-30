using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using System.Collections.Generic;
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

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = _context.Products.Select(p => new SelectListItem     //Agarra cada um dos produtos, cria um item (new SelectListItem) coloca lá o nome e o Id na forma de string  
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a product...)",
                Value = "0"
            });

            return list;

        }
    }
}
