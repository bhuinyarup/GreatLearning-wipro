using EcommerceApplication.Data;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET: /Checkout/Payment
        public async Task<IActionResult> Payment()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var cart = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (cart.Count == 0)
                return RedirectToAction("Index", "Cart");

            var total = cart.Sum(x => (x.Product?.Price ?? 0) * x.Quantity);

            ViewBag.Total = total;
            return View(cart);
        }

        // POST: /Checkout/Pay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var cart = await _db.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            if (cart.Count == 0)
                return RedirectToAction("Index", "Cart");

            // Generate nice order id
            var shortToken = Guid.NewGuid().ToString("N")[..6].ToUpper();
            var orderNo = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{shortToken}";

            var total = cart.Sum(x => (x.Product?.Price ?? 0) * x.Quantity);

            var order = new Order
            {
                OrderNumber = orderNo,
                UserId = user.Id,
                TotalAmount = total,
                PaymentStatus = "Paid",
                OrderStatus = "Placed"
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            // Add items
            foreach (var c in cart)
            {
                var price = c.Product?.Price ?? 0;

                _db.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    UnitPrice = price
                });
            }

            // Clear cart
            _db.CartItems.RemoveRange(cart);

            await _db.SaveChangesAsync();

            return RedirectToAction("Success", new { orderNumber = orderNo });
        }

        // GET: /Checkout/Success?orderNumber=...
        public IActionResult Success(string orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }
    }
}