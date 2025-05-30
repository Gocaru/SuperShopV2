using SuperShopV2.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName);     //Tarefa que devolve uma tabela de orders para ir buscar todas as encomendas de um determinado user


    }
}
