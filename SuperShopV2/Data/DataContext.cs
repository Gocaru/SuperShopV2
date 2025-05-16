using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;

namespace SuperShopV2.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }    //Propriedade responsável pela tabela

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
