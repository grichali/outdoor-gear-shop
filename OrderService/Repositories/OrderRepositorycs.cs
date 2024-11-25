using OrderService.Interfaces;
using OrderService.Model;

namespace OrderService.Repositories
{
    public class OrderRepositorycs : IOrderRepository
    {
        public Task<Order> confirmOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> createOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> deleteOrder(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> getBuyerOrder(string buyerId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> getSellerOrder(string sellerId)
        {
            throw new NotImplementedException();
        }
    }
}
