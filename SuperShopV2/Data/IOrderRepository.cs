using SuperShopV2.Data.Entities;
using SuperShopV2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    /// <summary>
    /// Interface que define os métodos específicos de repositório para a entidade Order.
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// Obtém todas as encomendas associadas a um determinado utilizador.
        /// </summary>
        /// <param name="userName">Nome do utilizador.</param>
        /// <returns>Consulta assíncrona que devolve as encomendas.</returns>
        Task<IQueryable<Order>> GetOrderAsync(string userName);     //Tarefa que devolve uma tabela de orders para ir buscar todas as encomendas de um determinado user


        /// <summary>
        /// Obtém todos os detalhes temporários de encomenda de um utilizador.
        /// </summary>
        /// <param name="userName">Nome do utilizador.</param>
        /// <returns>Consulta assíncrona que devolve os detalhes temporários.</returns>
        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        /// <summary>
        /// Adiciona um item à encomenda temporária do utilizador.
        /// </summary>
        /// <param name="model">Modelo com os dados do item a adicionar.</param>
        /// <param name="userName">Nome do utilizador.</param>
        Task AddItemToOrderAsync(AddItemViewModel model, string userName);  //Tarefa para adionar items

        /// <summary>
        /// Altera a quantidade de um item no detalhe temporário da encomenda.
        /// </summary>
        /// <param name="id">Identificador do item.</param>
        /// <param name="quantity">Nova quantidade.</param>
        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);   //Tarefa para modificar items


        /// <summary>
        /// Elimina um item do detalhe temporário da encomenda.
        /// </summary>
        /// <param name="id">Identificador do item a eliminar.</param>
        Task DeleteDetailTempAsync(int id);


        /// <summary>
        /// Confirma a encomenda atual do utilizador.
        /// </summary>
        /// <param name="userName">Nome do utilizador.</param>
        /// <returns>True se a encomenda for confirmada com sucesso, caso contrário false.</returns>
        Task<bool> ConfirmOrderAsync(string userName);  //Método para confirmar a encomenda


        /// <summary>
        /// Regista os dados de entrega de uma encomenda.
        /// </summary>
        /// <param name="model">Modelo com os dados de entrega.</param>
        Task DeliveryOrder(DeliveryViewModel model);//Método para colocar a data


        /// <summary>
        /// Obtém uma encomenda pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador da encomenda.</param>
        /// <returns>A encomenda correspondente ao identificador.</returns>
        Task<Order> GetOrderByIdAsync(int id);


    }
}
