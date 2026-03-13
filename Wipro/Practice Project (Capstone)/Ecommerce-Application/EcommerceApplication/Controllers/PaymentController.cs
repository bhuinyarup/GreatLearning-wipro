using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplication.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        // Keep the route /Payment working, but use Checkout flow
        public IActionResult Index() => RedirectToAction("Payment", "Checkout");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pay() => RedirectToAction("Pay", "Checkout");
    }
}