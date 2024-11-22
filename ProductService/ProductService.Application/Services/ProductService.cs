using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Application.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        private IImageRepository imageRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            return _productRepository.AddAsync(product, "hdked");
        }

        public Task DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(string id, ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}