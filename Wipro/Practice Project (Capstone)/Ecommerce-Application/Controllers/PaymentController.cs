using EcommerceApplication.Data;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplication.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show payment page
        public IActionResult Checkout()
        {
            return View();
        }

        // Fake payment processing
        [HttpPost]
        public async Task<IActionResult> PayNow()
        {
            var user = await _userManager.GetUserAsync(User);

            // Generate Order ID
            string orderId = "ORD-" + Guid.NewGuid().ToString().Substring(0, 8);

            var order = new Order
            {
                OrderNumber = orderId,
                UserId = user.Id,
                OrderDate = DateTime.Now,
                Status = "Paid"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", new { id = orderId });
        }

        public IActionResult Success(string id)
        {
            return View(model: id);
        }
    }
}