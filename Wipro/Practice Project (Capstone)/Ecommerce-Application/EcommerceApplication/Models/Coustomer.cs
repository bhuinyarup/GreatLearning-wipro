namespace EcommerceApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }   // PK
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}