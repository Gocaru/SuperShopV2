using Microsoft.WindowsAzure.Storage.Table.Protocol;
using SuperShopV2.Data.Entities;
using SuperShopV2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName);     //Tarefa que devolve uma tabela de orders para ir buscar todas as encomendas de um determinado user

        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);  //Tarefa para adionar items

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);   //Tarefa para modificar items

        Task DeleteDetailTempAsync(int id);


    }
}
