# TechShop 🛒

TechShop is a premium e-commerce web application built with ASP.NET Core MVC (.NET 10) and Entity Framework Core. 

Originally created as a university assignment, this project has undergone a **major architectural refactoring to meet modern industry standards**. The primary objective was to transform a standard MVC application into an enterprise-grade system by strictly adhering to **SOLID principles** and cleanly separating business logic from the presentation layer.

## 🚀 Key Features

* **Clean Architecture & SOLID Principles:** Shifted from legacy "Fat Controllers" to a robust Service Layer architecture driven by Dependency Injection. Dedicated interfaces (`ICartService`, `ICheckoutService`, `IProductCatalogService`, etc.) fully encapsulate the core business logic, ensuring high maintainability, testability, and strict separation of concerns.
* **Product Catalog & Dynamic Filtering:** Advanced product browsing featuring real-time text search, category-based filtering, and multi-criteria sorting (price and name).
* **Isolated Session Shopping Cart:** Seamless cart management using a custom session state handler. The shopping cart logic is completely encapsulated within a dedicated service layer, leaving controllers lightweight and tehermentesített.
* **Authentication & Security:** Powered by ASP.NET Core Identity. Includes custom real-time password strength verification, account/profile management, and fully operational Two-Factor Authentication (2FA).
* **Decoupled Checkout Workflow:** A secure checkout pipeline where independent services collaborate to validate, process, and securely persist cart items into finalized database orders.
* **Admin Dashboard (CRUD & Operations):** A role-protected management area driven by a dedicated admin service, allowing full control over the product catalog and real-time tracking/updating of customer orders.
* **Premium UI/UX:** High-end visual design incorporating glassmorphism effects, scroll-triggered typography animations (AOS), dynamic "Pro" product rendering, and asynchronous feedback via toast notifications.

## 💻 Tech Stack

* **Backend:** C#, ASP.NET Core MVC (.NET 10)
* **Architecture:** SOLID Principles, Dependency Injection (DI), Service Layer Pattern
* **Database:** Entity Framework Core (Code-First), MS SQL Server (LocalDB)
* **Frontend:** HTML5, Custom CSS (Heavily customized Bootstrap 5), Vanilla JS, AOS Animations
* **Auth:** ASP.NET Core Identity

