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

        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
        Task<Product> GetByIdAsync(string id);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetSellerProductsAsync(string sellerId);

    }
}
