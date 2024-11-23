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
        Task<ProductDto> UpdateProductAsync(string id, UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(string id);

    }

}
