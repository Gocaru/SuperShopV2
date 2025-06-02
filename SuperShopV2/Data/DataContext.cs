using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using System.Linq;

namespace SuperShopV2.Data
{
    /// <summary>
    /// Representa o contexto de dados principal da aplicação, incluindo tabelas relacionadas com utilizadores, produtos e encomendas.
    /// Herda de <see cref="IdentityDbContext{TUser}"/> para integração com o sistema de autenticação.
    /// </summary>
    public class DataContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Representa a tabela de produtos disponíveis na loja.
        /// </summary>
        public DbSet<Product> Products { get; set; }    //Propriedade responsável pela tabela products

        /// <summary>
        /// Representa a tabela de encomendas realizadas pelos utilizadores.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Representa os detalhes de cada encomenda, como produtos e quantidades.
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Representa os detalhes temporários de encomendas em fase de preparação ou checkout.
        /// </summary>
        public DbSet<OrderDetailTemp> OrderDetailsTemp { get; set; }

        public DbSet<Country> Countries { get; set; }


        public DbSet<City> Cities { get; set; }


        /// <summary>
        /// Inicializa uma nova instância de <see cref="DataContext"/> com as opções especificadas.
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        ///// <summary>
        ///// Configura o modelo de dados no momento da criação do contexto.
        ///// Esta implementação modifica o comportamento padrão de exclusão em cascata,
        ///// alterando-o para restrito em todas as relações não pertencentes.
        ///// </summary>
        ///// <param name="modelBuilder">Objeto que permite configurar o modelo através da API Fluent.</param>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)    //Este método é útil quando se pretende evitar exclusões em cascata (por exemplo, apagar uma entidade pai e, com isso, eliminar acidentalmente os filhos).
        //{
        //    // Obtém todas as chaves estrangeiras com comportamento de exclusão em cascata
        //    var cascadeFKs = modelBuilder.Model
        //        .GetEntityTypes()   //vou a todas as tabelas (as entidades)
        //        .SelectMany(t => t.GetForeignKeys())        //seleciono todas as chaves estrangeiras
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);       //Que tenham o comportamento de delete em cascata

        //    // Altera o comportamento de exclusão para "Restrict" em vez de "Cascade"
        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict;    //Não deixo apagar em cascata
        //    }

        //    // Chama a implementação base para garantir a configuração correta da identidade e outros elementos
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
