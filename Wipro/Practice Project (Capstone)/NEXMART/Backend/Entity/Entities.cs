namespace EcommerceApp.Entity
{
    public class Customer
    {
        private int _customerId;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;

        public Customer() { }
        public Customer(int customerId, string name, string email, string password)
        {
            _customerId = customerId; _name = name; _email = email; _password = password;
        }

        public int CustomerId    { get => _customerId; set => _customerId = value; }
        public string Name       { get => _name;       set => _name = value; }
        public string Email      { get => _email;      set => _email = value; }
        public string Password   { get => _password;   set => _password = value; }
    }

    public class Product
    {
        private int _productId;
        private string _name = string.Empty;
        private decimal _price;
        private string _description = string.Empty;
        private int _stockQuantity;
        private string _category = "General";   // ← NEW

        public Product() { }
        public Product(int productId, string name, decimal price, string description, int stockQuantity, string category = "General")
        {
            _productId = productId; _name = name; _price = price;
            _description = description; _stockQuantity = stockQuantity; _category = category;
        }

        public int ProductId       { get => _productId;    set => _productId = value; }
        public string Name         { get => _name;         set => _name = value; }
        public decimal Price       { get => _price;        set => _price = value; }
        public string Description  { get => _description;  set => _description = value; }
        public int StockQuantity   { get => _stockQuantity; set => _stockQuantity = value; }
        public string Category     { get => _category;     set => _category = value; }   // ← NEW
    }

    public class Cart
    {
        private int _cartId;
        private int _customerId;
        private int _productId;
        private int _quantity;

        public Cart() { }
        public Cart(int cartId, int customerId, int productId, int quantity)
        {
            _cartId = cartId; _customerId = customerId; _productId = productId; _quantity = quantity;
        }

        public int CartId      { get => _cartId;      set => _cartId = value; }
        public int CustomerId  { get => _customerId;  set => _customerId = value; }
        public int ProductId   { get => _productId;   set => _productId = value; }
        public int Quantity    { get => _quantity;    set => _quantity = value; }
        public Product? Product { get; set; }
    }

    public class Order
    {
        private int _orderId;
        private int _customerId;
        private DateTime _orderDate;
        private decimal _totalPrice;
        private string _shippingAddress = string.Empty;

        public Order() { }
        public Order(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            _orderId = orderId; _customerId = customerId; _orderDate = orderDate;
            _totalPrice = totalPrice; _shippingAddress = shippingAddress;
        }

        public int OrderId           { get => _orderId;          set => _orderId = value; }
        public int CustomerId        { get => _customerId;        set => _customerId = value; }
        public DateTime OrderDate    { get => _orderDate;         set => _orderDate = value; }
        public decimal TotalPrice    { get => _totalPrice;        set => _totalPrice = value; }
        public string ShippingAddress{ get => _shippingAddress;   set => _shippingAddress = value; }
        public List<OrderItem>? Items { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId     { get; set; }
        public int ProductId   { get; set; }
        public int Quantity    { get; set; }
        public Product? Product { get; set; }
    }
}
