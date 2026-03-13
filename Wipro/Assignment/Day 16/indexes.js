// products
 db.products.createIndex({ sku: 1 }, { unique: true });
 db.products.createIndex({ category: 1, isActive: 1, "price.amount": 1 });
 db.products.createIndex({ name: "text", brand: "text" });

// orders
 db.orders.createIndex({ userId: 1, orderDate: -1 });
 db.orders.createIndex({ orderNumber: 1 }, { unique: true });
 db.orders.createIndex({ "items.productId": 1, orderDate: -1 });

// users
 db.users.createIndex({ username: 1 }, { unique: true });
 db.users.createIndex({ email: 1 }, { unique: true });
