using EcommerceApp.Entity;

namespace EcommerceApp.Dao
{
    public interface IOrderProcessorRepository
    {
        // Product Management
        bool CreateProduct(Product product);
        bool DeleteProduct(int productId);
        List<Product> GetAllProducts();
        Product GetProductById(int productId);

        // Customer Management
        bool CreateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        List<Customer> GetAllCustomers();

        // Cart Management
        bool AddToCart(Customer customer, Product product, int quantity);
        bool RemoveFromCart(Customer customer, Product product);
        List<Cart> GetAllFromCart(Customer customer);

        // Order Management
        bool PlaceOrder(Customer customer, List<(Product product, int quantity)> items, string shippingAddress);
        List<Order> GetOrdersByCustomer(int customerId);
    }
}
