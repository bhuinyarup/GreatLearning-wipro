using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Dao;
using EcommerceApp.Entity;
using EcommerceApp.DTOs;
using EcommerceApp.MyExceptions;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IOrderProcessorRepository _repo;
        public ProductsController(IOrderProcessorRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _repo.GetAllProducts();
                return Ok(new ApiResponse<List<Product>>(true, "Products retrieved.", products));
            }
            catch (Exception ex) { return StatusCode(500, new ApiResponse<object>(false, ex.Message, null)); }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _repo.GetProductById(id);
                return Ok(new ApiResponse<Product>(true, "Product found.", product));
            }
            catch (ProductNotFoundException ex) { return NotFound(new ApiResponse<object>(false, ex.Message, null)); }
            catch (Exception ex) { return StatusCode(500, new ApiResponse<object>(false, ex.Message, null)); }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var product = new Product(0, dto.Name, dto.Price, dto.Description, dto.StockQuantity, dto.Category);
                bool result = _repo.CreateProduct(product);
                return Ok(new ApiResponse<bool>(true, "Product created successfully.", result));
            }
            catch (Exception ex) { return StatusCode(500, new ApiResponse<object>(false, ex.Message, null)); }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _repo.DeleteProduct(id);
                return Ok(new ApiResponse<bool>(true, "Product deleted.", result));
            }
            catch (ProductNotFoundException ex) { return NotFound(new ApiResponse<object>(false, ex.Message, null)); }
            catch (Exception ex) { return StatusCode(500, new ApiResponse<object>(false, ex.Message, null)); }
        }
    }
}
