# TechShop 🛒

TechShop is a premium e-commerce web application built with **ASP.NET Core MVC (.NET 10)** and **Entity Framework Core**. 

Originally created as a university assignment, this project goes beyond the basic requirements to deliver a modern, real-world shopping experience. My main goal was to combine solid backend architecture with a clean, high-end UI, proving that an MVC app doesn't have to look like a generic template.

## 🚀 Key Features

* **Product Catalog & Filtering:** Browse electronics, search by keyword, filter by category, and sort by price.
* **Session-based Shopping Cart:** Users can add items to their cart without logging in. The cart state is kept in the server memory until checkout.
* **Authentication & Security:** Built on ASP.NET Core Identity. It includes a custom real-time password strength checker, profile management, and fully working Two-Factor Authentication (2FA).
* **Checkout Workflow:** A secure checkout process that saves the finalized order and order items directly into the relational database.
* **Admin Dashboard:** A protected area for managing the product catalog (CRUD operations) and tracking customer orders.
* **Premium UI/UX:** Features glassmorphism effects, scroll-triggered typography animations, dynamic rendering for high-end "Pro" products, and asynchronous toast notifications.

## 💻 Tech Stack

* **Backend:** C#, ASP.NET Core MVC (.NET 10)
* **Database:** Entity Framework Core (Code-First), MS SQL Server (LocalDB)
* **Frontend:** HTML5, Custom CSS (Bootstrap 5 heavily customized), Vanilla JS, AOS Animations
* **Auth:** ASP.NET Core Identity

## 🛠️ How to run locally

1. Clone the repository and open the `.sln` file in Visual Studio 2022.
2. Open the **Package Manager Console** (`Tools > NuGet Package Manager`).
3. Run the `Update-Database` command to generate the local SQL tables.
4. Hit **F5** to build and run the application.
