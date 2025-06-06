﻿using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using SuperShopV2.Helpers;
using SuperShopV2.Models;
using System;
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

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName); //Verifico se o user existe
            if (user == null)
            {
                return;
            }

            var product = await _context.Products.FindAsync(model.ProductId);   //Verifico se o produto existe
            if (product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.OrderDetailsTemp               //Crio este objeto ("orderDetailTemp"), com os dados que recebo da tabela OrderDetailsTemp, em que o user e o produto foram os que foram passados e existem
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();

            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.OrderDetailsTemp.Add(orderDetailTemp);     //Crio o objeto na tabela (se não exitir nenhum)
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;             //Se já existe o produto, soma-se a quantidade
                _context.OrderDetailsTemp.Update(orderDetailTemp);      //E faço o update da tabela

            }

            await _context.SaveChangesAsync();  
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null) 
            { 
                return false; 
            }

            var orderTmps = await _context.OrderDetailsTemp
                .Include(o=> o.Product)
                .Where(o => o.User == user)
                .ToListAsync();

            if (orderTmps == null || orderTmps.Count == 0)
            {
                return false;
            }

            var details = orderTmps.Select(o => new OrderDetail     //Construo o Order Details (passo do order detail temporário para o OrderDetail)
            {
                Price=o.Price,
                Product = o.Product,
                Quantity = o.Quantity,
            }).ToList();

            var order = new Order       //Construo o Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                Items = details
            };

            await CreateAsync(order);
            _context.OrderDetailsTemp.RemoveRange(orderTmps);   //Removo o order details temporário
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null) 
            {
                return; 
            }

            _context.OrderDetailsTemp.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task DeliveryOrder(DeliveryViewModel model)
        {
            var order = await _context.Orders.FindAsync(model.Id);
            if(order == null)
            {
                return;
            }

            order.DeliveryDate = model.DeliveryDate;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();  
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
                    .Include(o => o.User)       //Porque preciso tb que o Admin tenha acesso ao nome do utilizador que fez a encomenda (na tabela Orders só tenho o UserId, não tenho o nome do user)
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

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.OrderDetailsTemp.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;
            if(orderDetailTemp.Quantity >0)     //Só vou guardar se a quantidade for maior do que zero
            {
                _context.OrderDetailsTemp.Update(orderDetailTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
