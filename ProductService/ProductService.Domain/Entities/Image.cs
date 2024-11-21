using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Entities
{
    public class Image
    {
        public string Id { get; set; }
        public string ProductId { get; set; } 
        public string Url { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
