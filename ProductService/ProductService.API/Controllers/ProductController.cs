using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Dtos;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interfaces;

namespace ProductService.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllProduct()
        {
            List<ProductDto> products = await _productService.GetAllProductsAsync();
            if(products.Count == 0)
            {
                return NotFound("no products");
            }
            return Ok(products);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto productDto)
        {
            ProductDto product = await _productService.CreateProductAsync(productDto);
            return Ok(product);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            bool result =await _productService.DeleteProductAsync(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("product can't be deleted");
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            ProductDto product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound("product not found");
            }
            return Ok(product);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string id ,[FromBody] UpdateProductDto productDto)
        {
            ProductDto product = await _productService.UpdateProductAsync(id,productDto);
            if(product == null)
            {
                return NotFound("product Not Found");
            }
            return Ok(product);
        }
    }
}
