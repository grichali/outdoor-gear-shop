using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using ProductService.Infrastructure.context;

namespace ProductService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext _db;
        public ProductRepository(MongoDbContext db)
        {
            _db = db;
        }
        public async Task<Product> AddAsync(Product product)
        {
            await _db.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Id, id);
            
            DeleteResult result = await _db.Products.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            IAsyncCursor<Product> cursor = await _db.Products.Find(Builders<Product>.Filter.Empty).ToCursorAsync();

            List<Product> products = await cursor.ToListAsync();

            return products;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(prod => prod.Id, id);

                        Product product = await _db.Products.Find(filter).FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<Product>> GetSellerProductsAsync(string sellerId)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.SellerId, sellerId);

            List<Product> products = await _db.Products.Find(filter).ToListAsync();

            return products;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);

                ReplaceOneResult result = await _db.Products.ReplaceOneAsync(filter, product);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return product;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
                return null;
            }
        }
    }
}
