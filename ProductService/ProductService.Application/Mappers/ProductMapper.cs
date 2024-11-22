using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Application.Dtos;
using ProductService.Domain.Entities;

namespace ProductService.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto toProductDto(this Product product, List<string> imageurls)
        {
            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                State = product.State,
                ImageUrls = imageurls
            };
        }
    }
}
