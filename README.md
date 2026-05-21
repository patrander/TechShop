TechShop 🛒
TechShop is a premium e-commerce web application built with ASP.NET Core MVC (.NET 10) and Entity Framework Core.

Originally created as a university assignment, this project has recently undergone a major architectural refactoring to meet modern industry standards. My main goal was to combine an enterprise-grade backend—strictly following SOLID principles—with a clean, high-end UI, proving that an MVC app doesn't have to look like a generic template.

🚀 Key Features

Clean Architecture & SOLID Principles: Shifted from "Fat Controllers" to a robust Service Layer architecture using Dependency Injection. Dedicated interfaces (ICartService, ICheckoutService, IProductCatalogService, etc.) handle complex business logic, ensuring high maintainability, testability, and a clear separation of concerns.

Product Catalog & Filtering: Browse electronics, search by keyword, filter by category, and sort by price.

Session-based Shopping Cart: Users can add items to their cart seamlessly. The cart state is managed via a custom session handler within an isolated service, completely decoupled from the UI controllers.

Authentication & Security: Built on ASP.NET Core Identity. It includes a custom real-time password strength checker, profile management, and fully working Two-Factor Authentication (2FA).

Checkout Workflow: A secure checkout process where independent services communicate with each other to securely transition cart items into finalized orders in the relational database.

Admin Dashboard: A role-protected area powered by a dedicated admin service for managing the product catalog (CRUD operations) and tracking customer orders.

Premium UI/UX: Features glassmorphism effects, scroll-triggered typography animations, dynamic rendering for high-end "Pro" products, and asynchronous toast notifications.

💻 Tech Stack

Backend: C#, ASP.NET Core MVC (.NET 10)

Architecture: SOLID Principles, Dependency Injection, Service Layer Pattern

Database: Entity Framework Core (Code-First), MS SQL Server (LocalDB)

Frontend: HTML5, Custom CSS (Bootstrap 5 heavily customized), Vanilla JS, AOS Animations

Auth: ASP.NET Core Identity

🛠️ How to run locally

Clone the repository and open the .sln file in Visual Studio 2022.

Open the Package Manager Console (Tools > NuGet Package Manager).

Run the Update-Database command to generate the local SQL tables.

Hit F5 to build and run the application.
