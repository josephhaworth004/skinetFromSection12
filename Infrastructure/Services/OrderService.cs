using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services 
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
               
        public OrderService(IGenericRepository<Order> orderRepo,
            IGenericRepository<DeliveryMethod> dmRepo, IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _dmRepo = dmRepo;
            _productRepo = productRepo;
            _basketRepo = basketRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int delieveryMethodId, string basketId, Address shippingAddress)
        {
            // Get basket from the basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // Need to check contents with what is in db - get the items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name,
                    productItem.PictureUrl);
                // productItem.Price gets the price form the db not the client basket
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // Get delivery method
            var delieveryMethod = await _dmRepo.GetByIdAsync(delieveryMethodId);
            
            // Calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            
            // Create order
            var order = new Order(items, buyerEmail, shippingAddress, delieveryMethod, subtotal);
            
            // Save to db
            // TODO!
            
            // return order
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}