using OrderService.Interfaces;
using OrderService.Model;

namespace OrderService.Services
{
    public class OrderServ : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderServ(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            return await _orderRepository.createOrder(order);
        }

        public async Task<Order> DeleteOrder(string id)
        {
            return await _orderRepository.deleteOrder(id);
        }

        public async Task<Order> GetBuyerOrder(string buyerId)
        {
            return await _orderRepository.getBuyerOrder(buyerId);
        }

        public async Task<Order> GetSellerOrder(string sellerId)
        {
            return await _orderRepository.getSellerOrder(sellerId);
        }

        public async Task<Order> ConfirmOrder(Order order)
        {
            return await _orderRepository.confirmOrder(order);
        }
    }
}
