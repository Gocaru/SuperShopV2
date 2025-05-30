using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using SuperShopV2.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository   //Tenho de ir buscar o "IOrderRepository" por causa das regras do Dependency Injection
                                                                                //No startUp defino que quando alguém pedir IProductRepository, fornece-lhe (injeta-lhe automaticamente) uma instância de OrderRepository
                                                                                //A classe OrderRepository tem obrigatoriamente de implementar a interface IOrderRepository

    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName); //Verifico sempre o user
            if (user == null)
            {
                return null;   
            }

            return _context.OrderDetailsTemp                // Vai à tabela Orders Details         
                .Include(p => p.Product)                    //Inclui todos os produtos
                .Where(o => o.User == user)                 //De um determinado user
                .OrderByDescending(o => o.Product.Name);    //Ordena pelo nome do Produto
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName) //Para ir buscar as encomendas
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return null;    //Retorna a lista vazia
            }
            
            if(await _userHelper.IsUserInRoleAsync(user, "Admin"))  //"O User que veio é Admin?"
            {
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }

            //Se não for Admin
            return _context.Orders      // Vai à tabela Orders         
                .Include (o => o.Items) //Inclui todos os items
                .ThenInclude(p => p.Product)    //E inclui todos os produtos
                .Where(o => o.User == user)     //De um determinado user
                .OrderByDescending(o => o.OrderDate);


        }
    }
}
