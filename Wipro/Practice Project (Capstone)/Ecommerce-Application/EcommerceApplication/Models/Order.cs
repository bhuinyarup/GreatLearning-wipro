using System.ComponentModel.DataAnnotations;

namespace EcommerceApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string OrderNumber { get; set; } = "";

        [Required]
        public string UserId { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        [Required]
        public string PaymentStatus { get; set; } = "Pending"; // Paid / Pending

        [Required]
        public string OrderStatus { get; set; } = "Placed"; // Placed / Shipped / Delivered etc.

        // Navigation
        public List<OrderItem> Items { get; set; } = new();
    }
}