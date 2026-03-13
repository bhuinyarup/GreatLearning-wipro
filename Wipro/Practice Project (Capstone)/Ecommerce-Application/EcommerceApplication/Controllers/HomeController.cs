using EcommerceApplication.Data;
using EcommerceApplication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApplication.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new HomeIndexVM
        {
            Categories = await _db.Categories.OrderBy(c => c.Name).ToListAsync(),
            Products = await _db.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Take(12)
                .ToListAsync(),
            CartCount = 0
        };

        if (User.Identity?.IsAuthenticated == true)
        {
            var userId = _userManager.GetUserId(User);
            vm.CartCount = await _db.CartItems.CountAsync(c => c.UserId == userId);
        }

        return View(vm);
    }

    public IActionResult Privacy() => View();
}