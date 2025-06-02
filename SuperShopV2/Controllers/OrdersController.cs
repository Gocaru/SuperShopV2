using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperShopV2.Data;
using SuperShopV2.Models;
using System;
using System.Threading.Tasks;

namespace SuperShopV2.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _orderRepository.GetOrderAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = _productRepository.GetComboProducts()    //Vou buscar todos os produtos para colocar dentro do select que está na view
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (ModelState.IsValid) 
            {
                await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return RedirectToAction("Create");

            }

            return View(model); 
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteDetailTempAsync(id.Value);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value,1);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await _orderRepository.ConfirmOrderAsync(this.User.Identity.Name);
            if(response)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }


        /// <summary>
        /// Apresenta o formulário para introdução da data de entrega de uma encomenda.
        /// </summary>
        /// <param name="id">Identificador da encomenda a ser entregue.</param>
        /// <returns>Vista com o formulário de entrega ou NotFound se a encomenda não existir.</returns>
        public async Task<IActionResult> Deliver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.GetByIdAsync(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            var model = new DeliveryViewModel
            {
                Id = order.Id,
                DeliveryDate = DateTime.Today
            };

            return View(model);
        }


        /// <summary>
        /// Regista a data de entrega da encomenda após submissão do formulário.
        /// </summary>
        /// <param name="model">Modelo com os dados da entrega.</param>
        /// <returns>Redireciona para a lista de encomendas se bem-sucedido; caso contrário, reapresenta a vista com o modelo inválido.</returns>
        [HttpPost]
        public async Task<IActionResult> Deliver(DeliveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.DeliveryOrder(model);
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}
