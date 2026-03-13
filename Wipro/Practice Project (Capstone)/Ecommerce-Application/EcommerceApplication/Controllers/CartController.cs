using EcommerceApplication.Data;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// updated
/// </summary>

namespace EcommerceApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        private static int GetQty(CartItem item)
        {
            var t = item.GetType();
            var p1 = t.GetProperty("Qty");
            if (p1 != null) return (int)(p1.GetValue(item) ?? 0);

            var p2 = t.GetProperty("Quantity");
            if (p2 != null) return (int)(p2.GetValue(item) ?? 0);

            return 0;
        }

        private static void SetQty(CartItem item, int value)
        {
            var t = item.GetType();
            var p1 = t.GetProperty("Qty");
            if (p1 != null) { p1.SetValue(item, value); return; }

            var p2 = t.GetProperty("Quantity");
            if (p2 != null) { p2.SetValue(item, value); return; }
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Redirect("/Identity/Account/Login");

            var items = await _db.CartItems
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == user.Id)
                .ToListAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int qty = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Redirect("/Identity/Account/Login");

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return NotFound();

            var item = await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == user.Id && c.ProductId == productId);

            if (item == null)
            {
                item = new CartItem
                {
                    UserId = user.Id,
                    ProductId = productId
                };
                SetQty(item, qty <= 0 ? 1 : qty);
                _db.CartItems.Add(item);
            }
            else
            {
                SetQty(item, GetQty(item) + (qty <= 0 ? 1 : qty));
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _db.CartItems.FindAsync(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}