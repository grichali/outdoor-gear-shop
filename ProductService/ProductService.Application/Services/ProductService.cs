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
        private IImageRepository _imageRepository;
        private readonly ICloudinary _cloudinary;
        public ProductService(IProductRepository productRepository, ICloudinary cloudinary, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _cloudinary = cloudinary;
            _imageRepository = imageRepository;
        }
        public Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            
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