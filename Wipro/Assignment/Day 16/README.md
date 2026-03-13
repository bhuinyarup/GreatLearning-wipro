# Assignment Day 16 - MongoDB Retail Catalog

Files in this package:
- products.sample.json
- orders.sample.json
- users.sample.json
- indexes.js
- queries.js

How this works:
1) Import sample JSON documents into MongoDB collections (`products`, `orders`, `users`).
2) Run `indexes.js` in Mongo shell to create indexes.
3) Run `queries.js` to execute common query patterns.

Why it will work:
- Uses hybrid schema design (embedded order item snapshots + references) suitable for large e-commerce systems.
- Compound indexes match filter + sort order in frequent lookups.
- Unique indexes protect key identity fields (`sku`, `orderNumber`, `username`, `email`).
