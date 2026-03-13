-- =============================================
-- EcommerceApp Database Setup Script
-- Run this in SSMS before starting the application
-- =============================================

CREATE DATABASE EcommerceDB;
GO

USE EcommerceDB;
GO

-- Customers Table
CREATE TABLE customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    email NVARCHAR(150) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL
);

-- Products Table
CREATE TABLE products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(150) NOT NULL,
    price DECIMAL(18,2) NOT NULL,
    description NVARCHAR(500),
    stockQuantity INT NOT NULL DEFAULT 0
);

-- Cart Table
CREATE TABLE cart (
    cart_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Cart_Customer FOREIGN KEY (customer_id) REFERENCES customers(customer_id) ON DELETE CASCADE,
    CONSTRAINT FK_Cart_Product FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

-- Orders Table
CREATE TABLE orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL DEFAULT GETDATE(),
    total_price DECIMAL(18,2) NOT NULL,
    shipping_address NVARCHAR(300) NOT NULL,
    CONSTRAINT FK_Orders_Customer FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

-- Order Items Table
CREATE TABLE order_items (
    order_item_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    CONSTRAINT FK_OrderItems_Order FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderItems_Product FOREIGN KEY (product_id) REFERENCES products(product_id)
);

-- Sample Data
INSERT INTO products (name, price, description, stockQuantity) VALUES
('Wireless Headphones', 89.99, 'Premium noise-cancelling wireless headphones with 30hr battery', 50),
('Mechanical Keyboard', 149.99, 'RGB backlit TKL mechanical keyboard with Cherry MX switches', 30),
('USB-C Hub', 49.99, '7-in-1 USB-C hub with HDMI, USB 3.0, SD card reader', 75),
('Laptop Stand', 35.00, 'Adjustable aluminum laptop stand for desks', 100),
('Webcam HD', 79.99, '1080p HD webcam with built-in microphone', 40),
('Mouse Pad XL', 24.99, 'Extended gaming mouse pad 900x400mm', 120);

INSERT INTO customers (name, email, password) VALUES
('Alice Johnson', 'alice@example.com', 'hashed_password_1'),
('Bob Smith', 'bob@example.com', 'hashed_password_2');

PRINT 'Database setup complete!';
GO
