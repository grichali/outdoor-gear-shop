using Contracts.EventContracts;
using MassTransit;
using OrderService.Events;
using OrderService.Interfaces;
using OrderService.Model;

namespace OrderService.Services
{
    public class OrderServ : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IBus bus;

        public OrderServ(IOrderRepository orderRepository, IBus bus)
        {
            _orderRepository = orderRepository;
            this.bus = bus;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            Order or = await _orderRepository.createOrder(order);
            IAddOrderEvent addOrderEvent = new AddOrderEvent() { productId = order.productId };
            await bus.Publish<IAddOrderEvent>(addOrderEvent);

            return order ;
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
