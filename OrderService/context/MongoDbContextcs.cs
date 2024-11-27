using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Model;

namespace OrderService.context
{
    public class MongoDbContextcs
    {
        private IMongoDatabase _database;

        public MongoDbContextcs(IOptions<MongoDbSettings> settings , IMongoClient _client)
        {
            _database = _client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Order> Images => _database.GetCollection<Order>("Orders");

    }
}
