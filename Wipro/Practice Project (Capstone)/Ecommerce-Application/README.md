🛒 E-Commerce Web Application

A full-stack E-Commerce Web Application built using ASP.NET Core MVC (.NET 8) that allows customers to browse products, manage shopping carts, and place orders while enabling administrators to manage categories and products through a secure admin panel.

The project demonstrates real-world MVC architecture, authentication systems, database integration, and responsive UI development using modern Microsoft technologies.

📌 Project Overview

This application simulates a complete online shopping workflow including:

User registration and login

Product browsing and categorization

Shopping cart management

Order processing

Admin dashboard for product and category management

The system follows the Model–View–Controller (MVC) design pattern to maintain separation of concerns between data, logic, and UI.

🧱 Technology Stack
Backend

ASP.NET Core MVC (.NET 8)

Entity Framework Core

ASP.NET Identity (Authentication & Role Management)

SQL Server / Azure SQL Database

Frontend

Razor Views

Bootstrap 5

Bootstrap Icons

HTML5 / CSS3

Development Tools

Visual Studio / VS Code

.NET CLI

Azure SQL Database

EF Core Migrations

🏗 Application Architecture

The project follows the MVC Architecture Pattern:

Layer	Responsibility
Model	Defines database entities and business data
View	UI pages built using Razor
Controller	Handles user requests and application logic

This structure improves maintainability, scalability, and separation of concerns.

🗄 Database Design

The application uses a Code-First approach with Entity Framework Core.

Main Entities

Category

Id

Name

Description

Product

Id

Name

Price

CategoryId (Foreign Key)

CartItem

Id

ProductId

UserId

Quantity

Order

Id

UserId

OrderDate

TotalAmount

OrderItem

Id

OrderId

ProductId

Quantity

👤 User Roles
🔧 Admin

Manage product categories (Create, Read, Update, Delete)

Manage products

Access admin dashboard

Secure admin routing using Areas

🛍 Customer

Register and login

Browse products

Add items to cart

Manage cart quantities

Checkout orders

🔐 Authentication & Authorization

Authentication is implemented using ASP.NET Identity.

Features include:

User registration and login

Role-based authorization

Admin and Customer roles

Secure authentication middleware

⚙️ Core Features
🧑‍💼 Admin Panel

Sidebar dashboard

Category management (CRUD)

Product management (CRUD)

Secure Admin Area routing

🛍 Customer Interface

Product listing page

Interactive product cards

Shopping cart system

Quantity updates

Order preparation workflow

🎨 UI Features

Fully responsive design using Bootstrap

Clean navigation layout

Product card hover animations

Admin dashboard interface

Interactive tables and forms

🔄 Database Operations

The application uses Entity Framework Core migrations.

dotnet ef migrations add EcommerceTables
dotnet ef database update

This allows automatic database schema generation and updates.

☁️ Azure Integration

The application supports Azure SQL Database, enabling:

Cloud database hosting

Remote database access

Seamless EF Core migration deployment

🚀 Application Workflow

User registers or logs into the system.

Products are displayed on the home page.

Customer adds products to the shopping cart.

Cart dynamically calculates totals.

Customer proceeds to checkout.

Admin manages products and categories through the Admin Dashboard.

🎯 Key Learning Outcomes

This project helped demonstrate practical experience with:

ASP.NET Core MVC architecture

Entity Framework Core relationships

Authentication and role-based authorization

Admin panel implementation using Areas

Razor View development

Database migrations and schema design

Full-stack web application development

📈 Future Enhancements

Payment Gateway Integration

Order History Page

Product Image Upload

Admin Analytics Dashboard

REST API implementation

Inventory management system

🏁 Conclusion

This project demonstrates the development of a real-world e-commerce platform using modern Microsoft technologies. It highlights backend development, database management, authentication systems, and responsive UI design while following industry-standard architectural practices.
