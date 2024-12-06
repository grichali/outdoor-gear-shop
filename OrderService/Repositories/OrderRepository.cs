using MongoDB.Driver;
using OrderService.Interfaces;
using OrderService.Model;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<Order> createOrder(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> deleteOrder(string id)
        {
            var objectId = new MongoDB.Bson.ObjectId(id); 
            var deleteResult = await _orders.DeleteOneAsync(o => o.Id == objectId);

            if (deleteResult.DeletedCount == 0)
            {
                return null; 
            }

            return new Order { Id = objectId }; 
        }

        public async Task<Order> getBuyerOrder(string buyerId)
        {
            return await _orders.Find(o => o.buyerId == buyerId).FirstOrDefaultAsync();
        }

        public async Task<Order> getSellerOrder(string sellerId)
        {
            return await _orders.Find(o => o.sellerId == sellerId).FirstOrDefaultAsync();
        }

        public async Task<Order> confirmOrder(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, order.Id);
            var update = Builders<Order>.Update.Set(o => o.status, "Confirmed");
            var result = await _orders.UpdateOneAsync(filter, update);

            if (result.ModifiedCount == 0)
            {
                return null; 
            }

            return await getBuyerOrder(order.buyerId);
        }
    }
}
