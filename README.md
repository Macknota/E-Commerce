# 🛒 E-Commerce API (Store.G02)

A robust, scalable RESTful API for an E-Commerce platform built with **.NET 8** following the **Clean Architecture** principles. This project demonstrates advanced backend patterns including the Specification Pattern, Repository Pattern, and distributed caching with Redis.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=flat&logo=nuget)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?style=flat&logo=redis)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat&logo=microsoft-sql-server)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat&logo=docker)

## 🏗️ Architecture

The solution implements **Clean Architecture (Onion Architecture)** to ensure separation of concerns and testability:

* **Core (Domain Layer):** Contains Entities, Enums, Interfaces, and Custom Exceptions. No external dependencies.
* **Services (Application Layer):** Business logic, Specifications, and Mapping profiles (AutoMapper).
* **Infrastructure:** Implementation of Repositories, Database Context (EF Core), Data Seeding, and Third-party integrations (Redis, Identity).
* **API (Presentation Layer):** Controllers, Middleware, and DI Configuration.

## ✨ Features

* **Catalog Management:**
    * Pagination, Sorting, Filtering, and Searching for products using **Specification Pattern**.
    * Brand and Type filtering.
* **Basket Management:**
    * High-performance basket storage using **Redis**.
    * Unique basket per user session.
* **Identity & Security:**
    * User Registration & Login (ASP.NET Core Identity).
    * JWT (JSON Web Token) Authentication.
    * Role-based Authorization.
    * Address management per user.
* **Orders:**
    * Create orders with validation.
    * Track order status.
    * Retrieve user-specific order history.
* **Performance:**
    * Custom Caching Attributes.
    * Redis distributed caching.
* **Error Handling:**
    * Global Exception Handling Middleware with standardized error responses.

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 8 Web API
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Caching:** Redis
* **Mapper:** AutoMapper
* **Documentation:** Swagger / OpenAPI

## 🚀 Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Redis](https://redis.io/download) (or run via Docker)

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/YourUsername/Store.G02.git](https://github.com/YourUsername/Store.G02.git)
    cd Store.G02
    ```

2.  **Configure AppSettings**
    Update `appsettings.json` in the API project with your connection strings:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=Store.G02.Db;Trusted_Connection=True;MultipleActiveResultSets=true",
      "RedisConnection": "localhost"
    }
    ```

3.  **Apply Migrations & Seed Data**
    The project is configured to apply migrations and seed initial data (Products, Brands, Types) automatically on startup.
    ```bash
    dotnet run --project Store.G02.Web
    ```

4.  **Explore the API**
    Navigate to `https://localhost:7295/swagger` to view the endpoints.

## 🧪 Future Improvements (To-Do)

* [ ] Add Unit Tests (xUnit & Moq).
* [ ] Implement Stripe Payment Gateway integration.
* [ ] Add Docker Support (Dockerfile & Compose).
* [ ] Implement Refresh Tokens.
* [ ] Add CI/CD Pipeline (GitHub Actions).

## 👤 Author

**Adel** * [LinkedIn](Your_LinkedIn_URL)
* [Portfolio](Your_Portfolio_URL)

---
*This project is part of my portfolio to demonstrate my skills in .NET Backend Development.*
