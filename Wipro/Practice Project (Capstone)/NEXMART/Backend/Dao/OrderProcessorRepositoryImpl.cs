using Microsoft.Data.SqlClient;
using EcommerceApp.Entity;
using EcommerceApp.MyExceptions;

namespace EcommerceApp.Dao
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {
        private readonly string _connectionString;

        public OrderProcessorRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        private SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        // ─── PRODUCT ───────────────────────────────────────────────

        public bool CreateProduct(Product product)
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "INSERT INTO products (name, price, description, stockQuantity, category) VALUES (@name, @price, @desc, @qty, @cat)",
                conn);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@desc", product.Description);
            cmd.Parameters.AddWithValue("@qty", product.StockQuantity);
            cmd.Parameters.AddWithValue("@cat", product.Category);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteProduct(int productId)
        {
            using var conn = GetConnection();
            var check = new SqlCommand("SELECT COUNT(1) FROM products WHERE product_id=@id", conn);
            check.Parameters.AddWithValue("@id", productId);
            if ((int)check.ExecuteScalar()! == 0) throw new ProductNotFoundException(productId);

            var cmd = new SqlCommand("DELETE FROM products WHERE product_id=@id", conn);
            cmd.Parameters.AddWithValue("@id", productId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<Product> GetAllProducts()
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "SELECT product_id, name, price, description, stockQuantity, category FROM products ORDER BY category, name",
                conn);
            using var reader = cmd.ExecuteReader();
            var list = new List<Product>();
            while (reader.Read()) list.Add(MapProduct(reader));
            return list;
        }

        public Product GetProductById(int productId)
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "SELECT product_id, name, price, description, stockQuantity, category FROM products WHERE product_id=@id",
                conn);
            cmd.Parameters.AddWithValue("@id", productId);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new ProductNotFoundException(productId);
            return MapProduct(reader);
        }

        // ─── CUSTOMER ──────────────────────────────────────────────

        public bool CreateCustomer(Customer customer)
        {
            using var conn = GetConnection();
            var hashed = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            var cmd = new SqlCommand(
                "INSERT INTO customers (name, email, password) VALUES (@name, @email, @pwd)", conn);
            cmd.Parameters.AddWithValue("@name", customer.Name);
            cmd.Parameters.AddWithValue("@email", customer.Email);
            cmd.Parameters.AddWithValue("@pwd", hashed);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteCustomer(int customerId)
        {
            using var conn = GetConnection();
            var check = new SqlCommand("SELECT COUNT(1) FROM customers WHERE customer_id=@id", conn);
            check.Parameters.AddWithValue("@id", customerId);
            if ((int)check.ExecuteScalar()! == 0) throw new CustomerNotFoundException(customerId);

            var cmd = new SqlCommand("DELETE FROM customers WHERE customer_id=@id", conn);
            cmd.Parameters.AddWithValue("@id", customerId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public Customer GetCustomerById(int customerId)
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "SELECT customer_id, name, email, password FROM customers WHERE customer_id=@id", conn);
            cmd.Parameters.AddWithValue("@id", customerId);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new CustomerNotFoundException(customerId);
            return MapCustomer(reader);
        }

        public List<Customer> GetAllCustomers()
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "SELECT customer_id, name, email, password FROM customers ORDER BY name", conn);
            using var reader = cmd.ExecuteReader();
            var list = new List<Customer>();
            while (reader.Read()) list.Add(MapCustomer(reader));
            return list;
        }

        // ─── CART ──────────────────────────────────────────────────

        public bool AddToCart(Customer customer, Product product, int quantity)
        {
            using var conn = GetConnection();
            var custCheck = new SqlCommand("SELECT COUNT(1) FROM customers WHERE customer_id=@id", conn);
            custCheck.Parameters.AddWithValue("@id", customer.CustomerId);
            if ((int)custCheck.ExecuteScalar()! == 0) throw new CustomerNotFoundException(customer.CustomerId);

            var prodCheck = new SqlCommand("SELECT COUNT(1) FROM products WHERE product_id=@id", conn);
            prodCheck.Parameters.AddWithValue("@id", product.ProductId);
            if ((int)prodCheck.ExecuteScalar()! == 0) throw new ProductNotFoundException(product.ProductId);

            var existing = new SqlCommand(
                "SELECT cart_id FROM cart WHERE customer_id=@cid AND product_id=@pid", conn);
            existing.Parameters.AddWithValue("@cid", customer.CustomerId);
            existing.Parameters.AddWithValue("@pid", product.ProductId);
            var existId = existing.ExecuteScalar();

            if (existId != null)
            {
                var update = new SqlCommand(
                    "UPDATE cart SET quantity = quantity + @qty WHERE cart_id=@id", conn);
                update.Parameters.AddWithValue("@qty", quantity);
                update.Parameters.AddWithValue("@id", existId);
                return update.ExecuteNonQuery() > 0;
            }

            var cmd = new SqlCommand(
                "INSERT INTO cart (customer_id, product_id, quantity) VALUES (@cid, @pid, @qty)", conn);
            cmd.Parameters.AddWithValue("@cid", customer.CustomerId);
            cmd.Parameters.AddWithValue("@pid", product.ProductId);
            cmd.Parameters.AddWithValue("@qty", quantity);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool RemoveFromCart(Customer customer, Product product)
        {
            using var conn = GetConnection();
            var cmd = new SqlCommand(
                "DELETE FROM cart WHERE customer_id=@cid AND product_id=@pid", conn);
            cmd.Parameters.AddWithValue("@cid", customer.CustomerId);
            cmd.Parameters.AddWithValue("@pid", product.ProductId);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0) throw new ProductNotFoundException($"Product {product.ProductId} not in cart.");
            return true;
        }

        public List<Cart> GetAllFromCart(Customer customer)
        {
            using var conn = GetConnection();
            var custCheck = new SqlCommand("SELECT COUNT(1) FROM customers WHERE customer_id=@id", conn);
            custCheck.Parameters.AddWithValue("@id", customer.CustomerId);
            if ((int)custCheck.ExecuteScalar()! == 0) throw new CustomerNotFoundException(customer.CustomerId);

            var cmd = new SqlCommand(@"
                SELECT c.cart_id, c.customer_id, c.product_id, c.quantity,
                       p.name, p.price, p.description, p.stockQuantity, p.category
                FROM cart c
                JOIN products p ON c.product_id = p.product_id
                WHERE c.customer_id = @cid", conn);
            cmd.Parameters.AddWithValue("@cid", customer.CustomerId);
            using var reader = cmd.ExecuteReader();
            var list = new List<Cart>();
            while (reader.Read())
            {
                list.Add(new Cart
                {
                    CartId = reader.GetInt32(0),
                    CustomerId = reader.GetInt32(1),
                    ProductId = reader.GetInt32(2),
                    Quantity = reader.GetInt32(3),
                    Product = new Product
                    {
                        ProductId = reader.GetInt32(2),
                        Name = reader.GetString(4),
                        Price = reader.GetDecimal(5),
                        Description = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        StockQuantity = reader.GetInt32(7),
                        Category = reader.IsDBNull(8) ? "General" : reader.GetString(8)
                    }
                });
            }
            return list;
        }

        // ─── ORDERS ────────────────────────────────────────────────

        public bool PlaceOrder(Customer customer, List<(Product product, int quantity)> items, string shippingAddress)
        {
            using var conn = GetConnection();
            using var tx = conn.BeginTransaction();
            try
            {
                decimal total = items.Sum(i => i.product.Price * i.quantity);

                var orderCmd = new SqlCommand(@"
                    INSERT INTO orders (customer_id, order_date, total_price, shipping_address)
                    OUTPUT INSERTED.order_id
                    VALUES (@cid, GETDATE(), @total, @addr)", conn, tx);
                orderCmd.Parameters.AddWithValue("@cid", customer.CustomerId);
                orderCmd.Parameters.AddWithValue("@total", total);
                orderCmd.Parameters.AddWithValue("@addr", shippingAddress);
                int orderId = (int)orderCmd.ExecuteScalar()!;

                foreach (var (product, qty) in items)
                {
                    var stockCmd = new SqlCommand(
                        "SELECT stockQuantity FROM products WHERE product_id=@id", conn, tx);
                    stockCmd.Parameters.AddWithValue("@id", product.ProductId);
                    int stock = (int)stockCmd.ExecuteScalar()!;
                    if (stock < qty) throw new InsufficientStockException(product.Name);

                    var itemCmd = new SqlCommand(@"
                        INSERT INTO order_items (order_id, product_id, quantity)
                        VALUES (@oid, @pid, @qty)", conn, tx);
                    itemCmd.Parameters.AddWithValue("@oid", orderId);
                    itemCmd.Parameters.AddWithValue("@pid", product.ProductId);
                    itemCmd.Parameters.AddWithValue("@qty", qty);
                    itemCmd.ExecuteNonQuery();

                    var updateStock = new SqlCommand(
                        "UPDATE products SET stockQuantity = stockQuantity - @qty WHERE product_id=@id",
                        conn, tx);
                    updateStock.Parameters.AddWithValue("@qty", qty);
                    updateStock.Parameters.AddWithValue("@id", product.ProductId);
                    updateStock.ExecuteNonQuery();
                }

                var clearCart = new SqlCommand(
                    "DELETE FROM cart WHERE customer_id=@cid", conn, tx);
                clearCart.Parameters.AddWithValue("@cid", customer.CustomerId);
                clearCart.ExecuteNonQuery();

                tx.Commit();
                return true;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            using var conn = GetConnection();
            var custCheck = new SqlCommand("SELECT COUNT(1) FROM customers WHERE customer_id=@id", conn);
            custCheck.Parameters.AddWithValue("@id", customerId);
            if ((int)custCheck.ExecuteScalar()! == 0) throw new CustomerNotFoundException(customerId);

            var cmd = new SqlCommand(@"
                SELECT o.order_id, o.customer_id, o.order_date, o.total_price, o.shipping_address,
                       oi.order_item_id, oi.product_id, oi.quantity,
                       p.name, p.price, p.description, p.stockQuantity, p.category
                FROM orders o
                JOIN order_items oi ON o.order_id = oi.order_id
                JOIN products p ON oi.product_id = p.product_id
                WHERE o.customer_id = @cid
                ORDER BY o.order_date DESC", conn);
            cmd.Parameters.AddWithValue("@cid", customerId);

            using var reader = cmd.ExecuteReader();
            var orders = new Dictionary<int, Order>();
            while (reader.Read())
            {
                int orderId = reader.GetInt32(0);
                if (!orders.ContainsKey(orderId))
                {
                    orders[orderId] = new Order
                    {
                        OrderId = orderId,
                        CustomerId = reader.GetInt32(1),
                        OrderDate = reader.GetDateTime(2),
                        TotalPrice = reader.GetDecimal(3),
                        ShippingAddress = reader.GetString(4),
                        Items = new List<OrderItem>()
                    };
                }
                orders[orderId].Items!.Add(new OrderItem
                {
                    OrderItemId = reader.GetInt32(5),
                    OrderId = orderId,
                    ProductId = reader.GetInt32(6),
                    Quantity = reader.GetInt32(7),
                    Product = new Product
                    {
                        ProductId = reader.GetInt32(6),
                        Name = reader.GetString(8),
                        Price = reader.GetDecimal(9),
                        Description = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        StockQuantity = reader.GetInt32(11),
                        Category = reader.IsDBNull(12) ? "General" : reader.GetString(12)
                    }
                });
            }
            return orders.Values.ToList();
        }

        // ─── Mappers ───────────────────────────────────────────────

        private static Product MapProduct(SqlDataReader r) => new()
        {
            ProductId     = r.GetInt32(0),
            Name          = r.GetString(1),
            Price         = r.GetDecimal(2),
            Description   = r.IsDBNull(3) ? "" : r.GetString(3),
            StockQuantity = r.GetInt32(4),
            Category      = r.IsDBNull(5) ? "General" : r.GetString(5)
        };

        private static Customer MapCustomer(SqlDataReader r) => new()
        {
            CustomerId = r.GetInt32(0),
            Name       = r.GetString(1),
            Email      = r.GetString(2),
            Password   = r.GetString(3)
        };
    }
}
