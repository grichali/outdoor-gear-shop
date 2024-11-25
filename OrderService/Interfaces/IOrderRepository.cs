using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Model;

namespace OrderService.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> createOrder(Order order);

        Task<Order> deleteOrder(string id);

        Task<Order> getSellerOrder(string sellerId);
        Task<Order> getBuyerOrder(string buyerId);

        Task<Order> confirmOrder(Order order);
    }
}