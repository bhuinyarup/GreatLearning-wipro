using EcommerceApplication.Models;

namespace EcommerceApplication.ViewModels;

public class HomeIndexVM
{
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public int CartCount { get; set; }
}