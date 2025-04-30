🛒 E-Commerce System (Onion Architecture)
A high-performance e-commerce platform built using ASP.NET Core and structured with the Onion Architecture to ensure maintainability, testability, and scalability. The system supports secure transactions, efficient data access, and delivers a smooth shopping experience for users.

🔍 Features
🧅 Clean Architecture (Onion Architecture) for separation of concerns

🔐 JWT Authentication & ASP.NET Identity for secure user management

💳 Payment Gateway Integration for real-world transaction simulation

🚀 Caching with Redis for enhanced performance and faster data retrieval

📦 Repository & Unit of Work Patterns for clean and testable data access

🧭 AutoMapper for clean object mapping

📄 Swagger UI for easy API exploration

📄 Specification Design Pattern for flexible and reusable queries

📚 Pagination to handle large datasets efficiently

🛠️ Tech Stack
Backend: ASP.NET Core (RESTful API)

Database: SQL Server

ORM: Entity Framework Core

Authentication: ASP.NET Identity + JWT

Caching: Redis

API Documentation: Swagger

Design Patterns: Specification, Repository, Unit of Work

Others: AutoMapper, Dependency Injection, Pagination

📂 Project Structure
The project follows the Onion Architecture:

Core – Domain models and interfaces

Application – Business logic, DTOs, service interfaces

Infrastructure – Data access (EF Core), external services

API – ASP.NET Core Web API (Controllers, Authentication, etc.)

📌 Notes
This project is a learning-oriented implementation of modern architecture and best practices in .NET development.

Future enhancements may include: front-end integration, order tracking, product reviews, and more.

📬 Contact
For any questions or suggestions, feel free to open an issue or contact me via GitHub
