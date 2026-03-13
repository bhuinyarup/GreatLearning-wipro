using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Dao;
using EcommerceApp.Entity;
using EcommerceApp.DTOs;
using EcommerceApp.MyExceptions;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IOrderProcessorRepository _repo;

        public CartController(IOrderProcessorRepository repo) => _repo = repo;

        [HttpGet("{customerId}")]
        public IActionResult GetCart(int customerId)
        {
            try
            {
                var customer = _repo.GetCustomerById(customerId);
                var cartItems = _repo.GetAllFromCart(customer);
                return Ok(new ApiResponse<List<Cart>>(true, "Cart retrieved.", cartItems));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddToCartDto dto)
        {
            try
            {
                var customer = _repo.GetCustomerById(dto.CustomerId);
                var product = _repo.GetProductById(dto.ProductId);
                bool result = _repo.AddToCart(customer, product, dto.Quantity);
                return Ok(new ApiResponse<bool>(true, "Product added to cart.", result));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        [HttpDelete("remove")]
        public IActionResult RemoveFromCart([FromBody] RemoveFromCartDto dto)
        {
            try
            {
                var customer = _repo.GetCustomerById(dto.CustomerId);
                var product = _repo.GetProductById(dto.ProductId);
                bool result = _repo.RemoveFromCart(customer, product);
                return Ok(new ApiResponse<bool>(true, "Product removed from cart.", result));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProcessorRepository _repo;

        public OrdersController(IOrderProcessorRepository repo) => _repo = repo;

        [HttpPost("place")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderDto dto)
        {
            try
            {
                var customer = _repo.GetCustomerById(dto.CustomerId);
                var items = new List<(Product product, int quantity)>();

                foreach (var item in dto.Items)
                {
                    var product = _repo.GetProductById(item.ProductId);
                    items.Add((product, item.Quantity));
                }

                bool result = _repo.PlaceOrder(customer, items, dto.ShippingAddress);
                return Ok(new ApiResponse<bool>(true, "Order placed successfully!", result));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (InsufficientStockException ex)
            {
                return BadRequest(new ApiResponse<object>(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }
    }
}
