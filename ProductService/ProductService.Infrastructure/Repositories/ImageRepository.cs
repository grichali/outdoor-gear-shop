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
    public class ImageRepository : IImageRepository
    {
        private readonly MongoDbContext _db;

        public ImageRepository(MongoDbContext db)
        {
             _db = db;
        }
        public async Task<Image> AddAsync(Image image)
        {
            await _db.Images.InsertOneAsync(image);

            return image;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<Image> filter = Builders<Image>.Filter.Eq(image => image.Id, id);

            DeleteResult result = await _db.Images.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<Image> GetByIdAsync(string imageId)
        {
            FilterDefinition<Image> filter = Builders<Image>.Filter.Eq(image => image.Id, imageId);

            Image image = await _db.Images.Find(filter).FirstOrDefaultAsync();

            return image;
        }

        public async Task<Image> UpadateAsync(Image image)
        {
            FilterDefinition<Image> filter = Builders<Image>.Filter.Eq(image => image.Id,image.Id);
            ReplaceOneResult result = await _db.Images.ReplaceOneAsync(filter, image);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return image;
            }

            return null;
        }
    }
}
