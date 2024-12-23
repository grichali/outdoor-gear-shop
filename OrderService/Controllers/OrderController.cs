using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;
using OrderService.Model;
using GrpcOrderToProduct;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        //private readonly GrpcProductService.GrpcProductServiceClient _grpcProductServiceClient;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            //_grpcProductServiceClient = grpcProductServiceClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var productRequest = new ProductRequest { ProductId = order.productId };
            //var productResponse = await _grpcProductServiceClient.CheckProductAvailabilityAsync(productRequest);

            //if (!productResponse.Available)
            //{
            //    return Conflict(new { message = "Product is not available." });
            //}
            Order createdOrder = await _orderService.CreateOrder(order);
            Console.WriteLine("the order that has been created is : "+ createdOrder.Id);
            Console.WriteLine("Order before return: " + JsonSerializer.Serialize(createdOrder));

            return Ok(createdOrder.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var deletedOrder = await _orderService.DeleteOrder(id);
            if (deletedOrder == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("buyer/{buyerId}")]
        public async Task<IActionResult> GetBuyerOrder(string buyerId)
        {
            var order = await _orderService.GetBuyerOrder(buyerId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("seller/{sellerId}")]
        public async Task<IActionResult> GetSellerOrder(string sellerId)
        {
            var order = await _orderService.GetSellerOrder(sellerId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmOrder([FromBody] Order order)
        {
            var updatedOrder = await _orderService.ConfirmOrder(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }
    }
}
