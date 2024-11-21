using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.Dtos;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(CreateProductDto request);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<List<ProductDto>> GetAllProductsAsync();
        Task UpdateProductAsync(string id, ProductDto productDto);
        Task DeleteProductAsync(string id);
    }

}
