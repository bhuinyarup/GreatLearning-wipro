🛒 Ecommerce Application (.NET 8 MVC)
📌 Project Overview

This project is a full-stack E-Commerce Web Application developed using ASP.NET Core MVC (.NET 8) with Entity Framework Core and Identity Authentication.

The application allows:

✅ Admin users to manage products and categories
✅ Customers to browse products
✅ Add items to cart
✅ Checkout using a dummy payment gateway
✅ Generate orders automatically
✅ Admin to view all placed orders

🚀 Technologies Used

ASP.NET Core MVC (.NET 8)

Entity Framework Core

SQL Server / Azure SQL

ASP.NET Identity (Authentication & Authorization)

Bootstrap 5 (UI Design)

Razor Views

LINQ

👤 User Roles
🔐 Admin

Manage Categories (CRUD)

Manage Products (CRUD)

View all Orders

Access Admin Dashboard

🛍 Customer

Register & Login

View Products

Add to Cart

Checkout

Dummy Payment

Order Creation

🧩 Application Modules
1️⃣ Authentication

User Registration

Login / Logout

Role-based access (Admin/User)

2️⃣ Category Management (Admin)

Create Category

Edit Category

Delete Category

View Categories

3️⃣ Product Management (Admin)

Add Products

Category dropdown mapping

Update/Delete Products

4️⃣ Shopping Cart

Add product to cart

Quantity management

Remove items

Cart total calculation

5️⃣ Checkout & Payment (Dummy Gateway)

Payment page simulation

Generates unique Order Number

Clears cart after payment

Example Order ID:

ORD-20260227-AB12CD
6️⃣ Order Management (Admin)

Admin can view:

Order Number

Customer

Order Date

Payment Status

Order Status

Ordered Products

🗂 Project Structure
EcommerceApplication
│
├── Areas
│   └── Admin
│       ├── Controllers
│       └── Views
│
├── Controllers
├── Models
├── Data
├── Views
├── ViewModels
└── wwwroot
⚙️ Database Setup

Run the following commands:

dotnet ef migrations add InitialCreate
dotnet ef database update
▶️ Run Project
dotnet build
dotnet run

Open browser:

http://localhost:5044
🔑 Admin Access

Assign admin role manually in database or seed data.

Admin-only quick links visible after login:

Categories

Orders

🎨 Features

✅ Responsive UI using Bootstrap
✅ Secure authentication
✅ Role-based authorization
✅ Clean MVC architecture
✅ Entity relationships using EF Core
✅ Interactive cart & checkout flow

📷 Screens Included

Home Page

Product Listing

Cart Page

Payment Gateway

Admin Dashboard

Orders Page

📚 Learning Outcomes

MVC Architecture

Entity Framework relationships

Identity Authentication

Razor Pages & Tag Helpers

CRUD Operations

Role-based UI rendering

👨‍💻 Author

Rupayan Bhuinya
Harsh Raj
Aakash Deep Sah
Swastik Padhy
Ketan Kumar


.NET Developer (Student Project)

📄 License

This project is created for educational purposes.