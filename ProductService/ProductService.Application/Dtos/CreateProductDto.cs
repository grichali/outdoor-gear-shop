using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Domain.Entities;
using ProductService.Domain.Enums;

namespace ProductService.Application.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductState State { get; set; }
        public ProductStatus Status { get; set; }
        public string SellerId { get; set; }
        public List<Image> ImageUrls { get; set; }
    }
}
