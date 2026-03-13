namespace EcommerceApp.MyExceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base("Customer not found.") { }
        public CustomerNotFoundException(int customerId) : base($"Customer with ID {customerId} not found.") { }
        public CustomerNotFoundException(string message) : base(message) { }
    }

    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Product not found.") { }
        public ProductNotFoundException(int productId) : base($"Product with ID {productId} not found.") { }
        public ProductNotFoundException(string message) : base(message) { }
    }

    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() : base("Order not found.") { }
        public OrderNotFoundException(int orderId) : base($"Order with ID {orderId} not found.") { }
        public OrderNotFoundException(string message) : base(message) { }
    }

    public class InsufficientStockException : Exception
    {
        public InsufficientStockException() : base("Insufficient stock available.") { }
        public InsufficientStockException(string productName) : base($"Insufficient stock for product: {productName}.") { }
    }
}
