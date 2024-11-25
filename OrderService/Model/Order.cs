using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Model
{
    public class Order
    {
        public int Id { get; set; }

        public string buyerId { get; set; }

        public string sellerId { get; set; }

        public string productId { get; set; }

        public string buyerReview { get; set; }

        public string status { get; set; }
    }
}