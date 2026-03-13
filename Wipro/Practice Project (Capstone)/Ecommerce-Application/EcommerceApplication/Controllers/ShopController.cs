using EcommerceApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShopController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index(int? categoryId)
        {
            var q = _db.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                q = q.Where(p => p.CategoryId == categoryId.Value);

            var products = await q.OrderBy(p => p.Name).ToListAsync();
            ViewBag.Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.SelectedCategoryId = categoryId;

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? NotFound() : View(product);
        }
    }
}