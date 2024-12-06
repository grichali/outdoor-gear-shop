using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderService.Model
{
    public class Order
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string buyerId { get; set; } = string.Empty;

        public string sellerId { get; set; } = string.Empty;

        public string productId { get; set; } = string.Empty;

        public string buyerReview { get; set; } = string.Empty;

        public string status { get; set; } = string.Empty;
    }
}