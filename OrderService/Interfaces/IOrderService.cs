using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<Order> DeleteOrder(string id);
        Task<Order> GetBuyerOrder(string buyerId);
        Task<Order> GetSellerOrder(string sellerId);
        Task<Order> ConfirmOrder(Order order);
    }
}