# 🛒 Store.G02 (E-Commerce REST API)

A robust, production-ready RESTful API for an E-Commerce platform built with **.NET 8**, following **Clean Architecture** principles. This project demonstrates advanced backend patterns including the Specification Pattern, Repository Pattern, Distributed Caching, and Containerization.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=flat&logo=nuget)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?style=flat&logo=redis)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat&logo=microsoft-sql-server)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat&logo=docker)
![License](https://img.shields.io/badge/License-MIT-green.svg)

---

## 🏗️ Architecture

The solution implements **Clean Architecture (Onion Architecture)** to ensure separation of concerns, scalability, and testability.

### 📂 Project Structure
```text
Store.G02
├── 📂 Core
│   ├── 📂 Store.G02.Domain      # Entities, Enums, Interfaces (No Dependencies)
│   └── 📂 Store.G02.Services    # Business Logic, Specifications, Mapper Profiles
├── 📂 Infrastructure
│   ├── 📂 Store.G02.Persistence # EF Core Context, Repositories, Data Seeding
│   └── 📂 Store.G02.Presentation# Controllers (Decoupled from Web Project)
└── 📂 Web
    └── 📂 Store.G02.Web         # API Entry Point, Middleware, DI Configuration
````

## ✨ Features

  * **Catalog Management:**
      * Advanced Pagination, Sorting, Filtering, and Searching using **Specification Pattern**.
      * Brand and Type filtering capabilities.
  * **Basket Management:**
      * High-performance basket storage using **Redis**.
      * Basket lifespan management (TTL).
  * **Identity & Security:**
      * Secure User Registration & Login (ASP.NET Core Identity).
      * **JWT (JSON Web Token)** Authentication & Claims-based Authorization.
      * Address management linked to user accounts.
  * **Order Processing:**
      * Order creation with business validation.
      * Order lifecycle tracking.
      * Payment Intent integration placeholder (Stripe).
  * **Performance:**
      * **Redis** distributed caching for basket data.
      * Custom **In-Memory Caching Attributes** for high-traffic endpoints.
  * **Error Handling:**
      * Centralized Global Exception Handling Middleware with standardized error responses (RFC 7807).

## 🛠️ Tech Stack

| Category | Technology |
|----------|------------|
| **Framework** | .NET 8 Web API |
| **Database** | SQL Server 2022 |
| **ORM** | Entity Framework Core (Code-First) |
| **Caching** | Redis |
| **Mapping** | AutoMapper |
| **Validation** | Data Annotations / Fluent Validation |
| **Containerization** | Docker |
| **Documentation** | Swagger / OpenAPI |

## 🔌 API Endpoints

Here are the main endpoints available in the API:

| Module | Method | Endpoint | Description |
| :--- | :--- | :--- | :--- |
| **Products** | `GET` | `/api/products` | Get all products with specs (sort, page, search). |
| | `GET` | `/api/products/{id}` | Get product details by ID. |
| **Basket** | `GET` | `/api/baskets` | Get user basket by ID. |
| | `POST` | `/api/baskets` | Create or update a basket. |
| **Account** | `POST` | `/api/auth/login` | Authenticate user and get JWT. |
| | `POST` | `/api/auth/register` | Register a new user. |
| **Orders** | `POST` | `/api/orders` | Create a new order. |
| | `GET` | `/api/orders` | Get all orders for the logged-in user. |

## 🚀 Getting Started

### Prerequisites

  * [.NET 8 SDK](https://dotnet.microsoft.com/download)
  * [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
  * [Redis](https://redis.io/download) (or run via Docker)

### Installation

1.  **Clone the repository**

    ```bash
    git clone [https://github.com/Macknota/Store.G02.git](https://github.com/Macknota/Store.G02.git)
    cd Store.G02
    ```

2.  **Configure AppSettings**
    Update `appsettings.json` in the `Store.G02.Web` project with your connection strings:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=Store.G02.Db;Trusted_Connection=True;TrustServerCertificate=True",
      "RedisConnection": "localhost"
    }
    ```

3.  **Apply Migrations & Run**
    The application is configured to automatically apply pending migrations and seed data on startup.

    ```bash
    dotnet watch --project Store.G02.Web
    ```

4.  **Explore the API**
    Navigate to `https://localhost:7295/swagger` to test the endpoints via Swagger UI.

## 🧪 Future Improvements (To-Do)

  * [ ] Implement **Unit Tests** using xUnit & Moq.
  * [ ] Full integration with **Stripe Payment Gateway**.
  * [ ] Add **SignalR** for real-time order status updates.
  * [ ] Implement Refresh Tokens for better security.
  * [ ] Add CI/CD Pipeline (GitHub Actions).

## 👤 Author

**Adel**

  * [LinkedIn](https://www.linkedin.com/in/adel-magdy-net/)

-----

*This project is part of my portfolio to demonstrate my skills in .NET Backend Development.*
