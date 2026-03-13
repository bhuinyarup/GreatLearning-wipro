using Moq;
using NUnit.Framework;
using EcommerceApp.Dao;
using EcommerceApp.Entity;
using EcommerceApp.MyExceptions;

namespace EcommerceApp.Tests
{
    [TestFixture]
    public class EcommerceTests
    {
        private Mock<IOrderProcessorRepository> _mockRepo = null!;
        private Customer _testCustomer = null!;
        private Product _testProduct = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IOrderProcessorRepository>();
            _testCustomer = new Customer(1, "Test User", "test@example.com", "password123");
            _testProduct = new Product(1, "Test Product", 99.99m, "A test product", 50);
        }

        // ─── TC1: Product Created Successfully ────────────────────

        [Test]
        public void CreateProduct_ValidProduct_ReturnsTrue()
        {
            // Arrange
            _mockRepo.Setup(r => r.CreateProduct(It.IsAny<Product>())).Returns(true);

            // Act
            bool result = _mockRepo.Object.CreateProduct(_testProduct);

            // Assert
            Assert.That(result, Is.True);
            _mockRepo.Verify(r => r.CreateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void CreateProduct_WithValidDetails_ProductIsCreated()
        {
            // Arrange
            var product = new Product(0, "Laptop", 999.99m, "A powerful laptop", 10);
            _mockRepo.Setup(r => r.CreateProduct(product)).Returns(true);

            // Act
            bool result = _mockRepo.Object.CreateProduct(product);

            // Assert
            Assert.IsTrue(result, "Product should be created successfully.");
        }

        // ─── TC2: Product Added to Cart Successfully ───────────────

        [Test]
        public void AddToCart_ValidCustomerAndProduct_ReturnsTrue()
        {
            // Arrange
            _mockRepo.Setup(r => r.AddToCart(_testCustomer, _testProduct, 2)).Returns(true);

            // Act
            bool result = _mockRepo.Object.AddToCart(_testCustomer, _testProduct, 2);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void AddToCart_ValidInput_AddsToCartSuccessfully()
        {
            // Arrange
            _mockRepo.Setup(r => r.AddToCart(
                It.Is<Customer>(c => c.CustomerId == 1),
                It.Is<Product>(p => p.ProductId == 1),
                It.IsAny<int>()
            )).Returns(true);

            // Act
            bool result = _mockRepo.Object.AddToCart(_testCustomer, _testProduct, 3);

            // Assert
            Assert.IsTrue(result, "Product should be added to cart successfully.");
            _mockRepo.Verify(r => r.AddToCart(_testCustomer, _testProduct, 3), Times.Once);
        }

        // ─── TC3: Order Placed Successfully ───────────────────────

        [Test]
        public void PlaceOrder_ValidOrder_ReturnsTrue()
        {
            // Arrange
            var items = new List<(Product product, int quantity)>
            {
                (_testProduct, 2)
            };
            _mockRepo.Setup(r => r.PlaceOrder(_testCustomer, items, "123 Main St")).Returns(true);

            // Act
            bool result = _mockRepo.Object.PlaceOrder(_testCustomer, items, "123 Main St");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void PlaceOrder_ValidCartItems_OrderPlacedSuccessfully()
        {
            // Arrange
            var items = new List<(Product product, int quantity)>
            {
                (new Product(1, "Keyboard", 49.99m, "Mechanical keyboard", 20), 1),
                (new Product(2, "Mouse", 29.99m, "Wireless mouse", 15), 2)
            };
            _mockRepo.Setup(r => r.PlaceOrder(
                It.IsAny<Customer>(),
                It.IsAny<List<(Product, int)>>(),
                It.IsAny<string>()
            )).Returns(true);

            // Act
            bool result = _mockRepo.Object.PlaceOrder(_testCustomer, items, "456 Oak Avenue");

            // Assert
            Assert.IsTrue(result, "Order should be placed successfully.");
        }

        // ─── TC4: Exception Thrown When IDs Not Found ─────────────

        [Test]
        public void GetCustomerById_InvalidId_ThrowsCustomerNotFoundException()
        {
            // Arrange
            int invalidCustomerId = 9999;
            _mockRepo.Setup(r => r.GetCustomerById(invalidCustomerId))
                     .Throws(new CustomerNotFoundException(invalidCustomerId));

            // Act & Assert
            var ex = Assert.Throws<CustomerNotFoundException>(
                () => _mockRepo.Object.GetCustomerById(invalidCustomerId)
            );
            Assert.That(ex.Message, Does.Contain("9999"));
        }

        [Test]
        public void GetProductById_InvalidId_ThrowsProductNotFoundException()
        {
            // Arrange
            int invalidProductId = 8888;
            _mockRepo.Setup(r => r.GetProductById(invalidProductId))
                     .Throws(new ProductNotFoundException(invalidProductId));

            // Act & Assert
            var ex = Assert.Throws<ProductNotFoundException>(
                () => _mockRepo.Object.GetProductById(invalidProductId)
            );
            Assert.That(ex.Message, Does.Contain("8888"));
        }

        [Test]
        public void DeleteCustomer_NonExistentId_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteCustomer(999))
                     .Throws(new CustomerNotFoundException(999));

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(
                () => _mockRepo.Object.DeleteCustomer(999)
            );
        }

        [Test]
        public void DeleteProduct_NonExistentId_ThrowsProductNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteProduct(999))
                     .Throws(new ProductNotFoundException(999));

            // Act & Assert
            Assert.Throws<ProductNotFoundException>(
                () => _mockRepo.Object.DeleteProduct(999)
            );
        }

        [Test]
        public void GetOrdersByCustomer_InvalidCustomer_ThrowsCustomerNotFoundException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetOrdersByCustomer(0))
                     .Throws(new CustomerNotFoundException(0));

            // Act & Assert
            Assert.Throws<CustomerNotFoundException>(
                () => _mockRepo.Object.GetOrdersByCustomer(0)
            );
        }

        // ─── Additional Edge Cases ─────────────────────────────────

        [Test]
        public void GetAllFromCart_ValidCustomer_ReturnsCartItems()
        {
            // Arrange
            var cartItems = new List<Cart>
            {
                new Cart(1, 1, 1, 2) { Product = _testProduct }
            };
            _mockRepo.Setup(r => r.GetAllFromCart(_testCustomer)).Returns(cartItems);

            // Act
            var result = _mockRepo.Object.GetAllFromCart(_testCustomer);

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].ProductId, Is.EqualTo(1));
        }

        [Test]
        public void GetOrdersByCustomer_ValidId_ReturnsOrderList()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order(1, 1, DateTime.Now, 199.98m, "123 Main St")
            };
            _mockRepo.Setup(r => r.GetOrdersByCustomer(1)).Returns(orders);

            // Act
            var result = _mockRepo.Object.GetOrdersByCustomer(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].TotalPrice, Is.EqualTo(199.98m));
        }
    }
}
