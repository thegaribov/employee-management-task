# About system
Basic employee management system. Features :
1. Create new department
2. Edit department details
3. Delete department from system
4. Get all departments
5. Create new employee
6. Edit employee details
7. View details of employee
8. Delete employee from system
9. Get all employee and apply filters

# Technical Stack
- ASP.NET Core 6.0 (with .NET Core 6.0)
- Entity Framework Core
- .NET Core Native DI
- FluentValidator
- AutoMapper
- SQL Server
- Swagger UI
- Serilog
- XUnit (+ FluentAssertions)

# Design principles & patterns
- Unit Of Work
- Repository & Generic Repository
- ORM
- Unit testing
- Elegant Exception handling
- Single responsibility principle
- Dependency Inversion principle
- High cohesion and loosely coupling
- Open closed principle
- Mediator
- Command–query separation
- Options pattern
- Inversion of Control / Dependency injection

# Sotware architecture
- N-tier architecture

# Prerequirements
- Visual Studio 2019+
- .NET Core 5
- EF Core
- Docker
- Docker-compose

# How To Run
- Clone the repository and open `employee-management` folder
- Docker desktop should be active in Windows OS
- Run docker: `docker-compose -f _development/docker-compose.yml up --build -d`
- Application will be started on: `http://localhost:5000/` and you can check APIs on `http://localhost:5000/swagger`
- I added an additional container (`portrainer`) to manage other containers, so you can check it :  `http://localhost:9000/`  
