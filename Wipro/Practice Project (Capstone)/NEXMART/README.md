# NEXMART — Ecommerce Application
### .NET 8 Web API + SQL Server + Vanilla JS Frontend

---

## 📁 Project Structure

```
EcommerceApp/
├── Backend/                        ← .NET 8 Web API
│   ├── Controllers/
│   │   ├── ProductsController.cs
│   │   ├── CustomersController.cs
│   │   └── CartOrderController.cs
│   ├── Dao/
│   │   ├── IOrderProcessorRepository.cs   ← Interface
│   │   └── OrderProcessorRepositoryImpl.cs ← SQL Server impl
│   ├── DTOs/
│   │   └── Dtos.cs
│   ├── Entity/
│   │   └── Entities.cs            ← Customer, Product, Cart, Order, OrderItem
│   ├── MyExceptions/
│   │   └── Exceptions.cs          ← Custom exceptions
│   ├── Util/
│   │   └── DBUtil.cs              ← DBPropertyUtil + DBConnUtil
│   ├── Program.cs
│   ├── appsettings.json
│   ├── database_setup.sql
│   └── EcommerceApp.csproj
├── Tests/
│   ├── EcommerceTests.cs          ← NUnit + Moq test cases
│   └── EcommerceApp.Tests.csproj
├── Frontend/
│   └── index.html                 ← Single-file UI (open in browser)
└── EcommerceApp.sln
```

---

## 🚀 Setup Instructions

### Step 1 — Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**
2. Connect to your SQL Server instance
3. Open `Backend/database_setup.sql`
4. Execute the script (creates `EcommerceDB` with all tables + sample data)

### Step 2 — Configure Connection String

Edit `Backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=EcommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Common server names:**
- Local default instance: `localhost` or `.\SQLEXPRESS`
- Named instance: `localhost\SQLEXPRESS`
- With port: `localhost,1433`

### Step 3 — Run the API

```bash
cd Backend
dotnet restore
dotnet run
```

API will start at `http://localhost:5000` (or `https://localhost:7000`)

- **Swagger UI:** `http://localhost:5000/swagger`

### Step 4 — Open the Frontend

Simply open `Frontend/index.html` in your browser.

> If CORS errors appear, ensure the API is running and the port in `index.html` (`const API = 'http://localhost:5000/api'`) matches your API port.

### Step 5 — Run Unit Tests

```bash
cd Tests
dotnet restore
dotnet test --verbosity normal
```

---

## 🌐 API Endpoints

### Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create product |
| DELETE | `/api/products/{id}` | Delete product |

### Customers
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/customers` | Get all customers |
| GET | `/api/customers/{id}` | Get customer by ID |
| POST | `/api/customers/register` | Register customer |
| DELETE | `/api/customers/{id}` | Delete customer |
| GET | `/api/customers/{id}/orders` | Get customer orders |

### Cart
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/cart/{customerId}` | View cart |
| POST | `/api/cart/add` | Add to cart |
| DELETE | `/api/cart/remove` | Remove from cart |

### Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/orders/place` | Place order |

---

## ✅ Unit Tests Covered

1. **Product created successfully** — verifies CreateProduct returns true
2. **Product added to cart** — verifies AddToCart returns true
3. **Order placed successfully** — verifies PlaceOrder returns true
4. **CustomerNotFoundException** thrown for invalid customer ID
5. **ProductNotFoundException** thrown for invalid product ID
6. **Cart retrieval** returns correct items
7. **Order retrieval** by customer ID
8. Additional edge cases for Delete operations

---

## 🏛️ Architecture

```
Frontend (index.html)
     ↓ HTTP/REST
Controllers (ASP.NET Core Web API)
     ↓
IOrderProcessorRepository (interface)
     ↓
OrderProcessorRepositoryImpl (SQL Server via ADO.NET)
     ↓
SQL Server (EcommerceDB)
```

**Custom Exceptions:**
- `CustomerNotFoundException` — invalid customer ID
- `ProductNotFoundException` — invalid product ID  
- `OrderNotFoundException` — invalid order ID
- `InsufficientStockException` — not enough stock

---

## 📝 Notes

- Passwords are hashed using **BCrypt** before storing
- All DB operations use **parameterized queries** (SQL injection safe)
- Order placement uses **transactions** for atomicity
- Stock is automatically decremented on order placement
- Cart is cleared automatically after successful order
