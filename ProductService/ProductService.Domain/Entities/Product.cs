using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductService.Domain.Enums;

namespace ProductService.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductState State { get; set; }

        public ProductStatus Status { get; set; }
        public string SellerId { get; set; } = string.Empty;
        public List<string> ImageIds { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
