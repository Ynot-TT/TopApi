using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopStyle.Domain.Entities;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/api/all-products")]
        public async Task<ActionResult<ProductDTO>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("/api/product/{productId}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return BadRequest("No product with that id");
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("/api/produts/{title}")]
        public async Task<ActionResult<ProductDTO>> GetProductByTitle(string title)
        {
            var products = await _productService.GetProductByTitleAsync(title);

            if (products == null || products.Count() <= 0)
            {
                return NotFound("No products found with that title");
            }
            
            return Ok(products);
        }

        [HttpPost]
        [Route("/api/add-product")]
        public async Task<ActionResult<Product>> AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid parameter");
            }
            await _productService.AddProductAsync(productDTO);  
            
            return Ok("Product added");
        }
    }
}
