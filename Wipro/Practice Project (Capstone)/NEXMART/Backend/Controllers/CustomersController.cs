using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Dao;
using EcommerceApp.Entity;
using EcommerceApp.DTOs;
using EcommerceApp.MyExceptions;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IOrderProcessorRepository _repo;

        public CustomersController(IOrderProcessorRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var customers = _repo.GetAllCustomers();
                // Don't expose passwords
                customers.ForEach(c => c.Password = "");
                return Ok(new ApiResponse<List<Customer>>(true, "Customers retrieved.", customers));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var customer = _repo.GetCustomerById(id);
                customer.Password = "";
                return Ok(new ApiResponse<Customer>(true, "Customer found.", customer));
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

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateCustomerDto dto)
        {
            try
            {
                var customer = new Customer(0, dto.Name, dto.Email, dto.Password);
                bool result = _repo.CreateCustomer(customer);
                return Ok(new ApiResponse<bool>(true, "Customer registered successfully.", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<object>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _repo.DeleteCustomer(id);
                return Ok(new ApiResponse<bool>(true, "Customer deleted.", result));
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

        [HttpGet("{id}/orders")]
        public IActionResult GetOrders(int id)
        {
            try
            {
                var orders = _repo.GetOrdersByCustomer(id);
                return Ok(new ApiResponse<List<Order>>(true, "Orders retrieved.", orders));
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
    }
}
