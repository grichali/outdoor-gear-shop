using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoDbSettings> settings, IMongoClient _client)
        {
            _database = _client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Image> Images => _database.GetCollection<Image>("Images");

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

    }
}
