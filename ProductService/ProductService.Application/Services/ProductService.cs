using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProductService.Application.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Application.Mappers;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;
using Grpc.Core;
using GrpcOrderToProduct;

namespace ProductService.Application.Services
{
    public class ProductServ : GrpcProductService.GrpcProductServiceBase, IProductService

    {
        private IProductRepository _productRepository;
        private IImageRepository _imageRepository;
        private readonly ICloudinaryService _cloudinary;
        private readonly ICacheService _cacheService;

        public ProductServ(IProductRepository productRepository, ICloudinaryService cloudinary, IImageRepository imageRepository,ICacheService cacheService)
        {
            _productRepository = productRepository;
            _cloudinary = cloudinary;
            _imageRepository = imageRepository;
            _cacheService = cacheService;
        }

        public override async Task<ProductResponse> CheckProductAvailability(ProductRequest request, ServerCallContext context)
        {
            Product product = await _productRepository.GetByIdAsync(request.ProductId);

            bool isAvailable = product != null;

            return new ProductResponse { Available = isAvailable };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            List<string> imagesid = new List<string>();
            foreach (IFormFile image in productDto.ImageUrls)
            {
                string imageurl = await _cloudinary.UploadImageAsync(image.OpenReadStream());
                Image newimage = new Image
                {
                    Url = imageurl,
                };
                newimage = await _imageRepository.AddAsync(newimage);
                imagesid.Add(newimage.Id);
            }

            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                State = productDto.State,
                Status = productDto.Status,
                ImageIds = imagesid,
                SellerId = productDto.SellerId,
            };

            Product newproduct = await _productRepository.AddAsync(product);
            List<string> imageurls = new List<string>();
            foreach (string id in product.ImageIds)
            {
                Image image = await _imageRepository.GetByIdAsync(id);
                imageurls.Add(image.Url);
            }
            return newproduct.toProductDto(imageurls);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {

            Product result = await _productRepository.GetByIdAsync(id);
            if (result != null)
            {
                foreach (string imageid in result.ImageIds)
                {
                    Image image = await _imageRepository.GetByIdAsync(imageid);

                    bool deleteImage = await _cloudinary.DeleteImageAsync(image.Url);
                    if (!deleteImage)
                    {
                        return false;
                    }
                    await _imageRepository.DeleteAsync(image.Id);
                }

                return await _productRepository.DeleteAsync(result.Id);

            }

            return false;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in products)
            {
                ProductDto productDto = await GetProductByIdAsync(product.Id);
                productDtos.Add(productDto);
            }
            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(string id)
        {
            ProductDto? cachedProduct = await _cacheService.GetAsync<ProductDto>(id);
            if (cachedProduct != null)
            {
                return cachedProduct;
            }

            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            List<string> imageurls = new List<string>();
            foreach (string imageId in product.ImageIds)
            {
                Image image = await _imageRepository.GetByIdAsync(imageId);
                imageurls.Add(image.Url);
            }

            ProductDto productDto = product.toProductDto(imageurls);

            await _cacheService.SetAsync(id, productDto, TimeSpan.FromMinutes(10));

            return productDto;
        }

        public async Task<ProductDto> UpdateProductAsync(string id, UpdateProductDto productDto)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            List<string> imagesToDelete = product.ImageIds.Except(productDto.imageIds).ToList();
            List<string> imageToKeep = product.ImageIds.Intersect(productDto.imageIds).ToList();
            foreach (string imageId in imagesToDelete)
            {
                await _imageRepository.DeleteAsync(imageId);
            }
            foreach (string imageId in imageToKeep)
            {
                Image image = await _imageRepository.GetByIdAsync(imageId);
                if (image != null && !productDto.ImageUrls.Contains(image.Url))
                {
                    image.Url = productDto.ImageUrls.FirstOrDefault(url => url == image.Url) ?? image.Url;
                    await _imageRepository.UpadateAsync(image);
                }
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.State = productDto.State;
            product.ImageIds = productDto.imageIds;
            await _productRepository.UpdateAsync(product);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                State = product.State,
                ImageUrls = product.ImageIds
                    .Select(async id => (await _imageRepository.GetByIdAsync(id))?.Url)
                    .Where(task => task.Result != null)
                    .Select(task => task.Result)
                    .ToList()
            };

        }
    }
}