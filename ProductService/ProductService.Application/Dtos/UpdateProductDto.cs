using ProductService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Dtos
{
    public class UpdateProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductState State { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> imageIds { get; set; }
    }
}
