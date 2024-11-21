using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllAsync();

        Task<List<Product>> GetSellerProductsAsync(string sellerId);
        Task AddAsync(Product product, string sellerId);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
    }
}
