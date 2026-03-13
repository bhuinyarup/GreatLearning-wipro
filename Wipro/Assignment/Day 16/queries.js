// A) Product list by category, active only, sorted by price
 db.products.find(
   { category: "Electronics", isActive: true },
   { name: 1, sku: 1, "price.amount": 1 }
 ).sort({ "price.amount": 1 }).limit(20);

// B) Orders for a specific user (most recent first)
 db.orders.find(
   { userId: ObjectId("65f300000000000000000901") },
   { orderNumber: 1, orderDate: 1, status: 1, costSummary: 1 }
 ).sort({ orderDate: -1 }).limit(10);

// C) Authenticate user by email lookup (password check done in app)
 db.users.findOne(
   { email: "jane@example.com" },
   { email: 1, passwordHash: 1, roles: 1, isEmailVerified: 1 }
 );

// D) Find orders containing a given product
 db.orders.find(
   { "items.productId": ObjectId("65f100000000000000000001") },
   { orderNumber: 1, userId: 1, orderDate: 1 }
 ).sort({ orderDate: -1 });
