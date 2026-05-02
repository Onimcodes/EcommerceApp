# E-Commerce Microservices Application

A modern e-commerce platform built with .NET 10 using microservices architecture. The system demonstrates clean architecture principles, CQRS pattern, and event-driven communication between services.

## Table of Contents

- [Architecture Overview](#architecture-overview)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [API Endpoints](#api-endpoints)
- [Architectural Decisions](#architectural-decisions)
- [Trade-offs Made](#trade-offs-made)
- [Future Improvements](#future-improvements)

---

## Architecture Overview

This solution consists of three main microservices:

### 1. **IdentityService** (Authentication & Authorization)
- Handles user registration and authentication
- Issues JWT tokens for API access
- Password hashing with BCrypt
- Refresh token management
- Clean Architecture + CQRS pattern

### 2. **ProductService** (Product Catalog)
- Manages product inventory
- Listens to order events via RabbitMQ
- Updates stock levels based on orders
- Exposes product management APIs

### 3. **OrderService** (Order Management)
- Handles order creation and management
- Publishes order events (OrderCreated) to message bus
- Communicates with other services via async messaging

### Inter-Service Communication
- **Async Communication**: RabbitMQ with MassTransit for event-driven architecture
- **Each service has its own database**: Database per service pattern

---

## Project Structure

```
EcommerceApp/
├── IdentityService/
│   ├── src/
│   │   ├── core/
│   │   │   ├── IdentityService.domain/          # Domain entities
│   │   │   └── IdentityService.application/     # CQRS commands, handlers, DTOs
│   │   │       └── AuthModule/
│   │   │           ├── Commands/
│   │   │           │   ├── RegisterUser/
│   │   │           │   └── Login/
│   │   │           └── Dtos/
│   │   └── external/
│   │       ├── IdentityService.api/             # API layer (controllers)
│   │       └── IdentityService.infrastructure/  # Data access, authentication
│   │           ├── Configuration/
│   │           ├── Persistence/
│   │           ├── Authentication/
│   │           └── Migrations/
│   │
├── OrderService/
│   ├── src/
│   │   ├── core/
│   │   │   ├── OrderService.domain/
│   │   │   └── OrderService.application/
│   │   └── external/
│   │       ├── OrderService.api/
│   │       └── OrderService.infrastructure/
│   │
├── ProductService/
│   ├── src/
│   │   ├── core/
│   │   │   ├── ProductService.domain/
│   │   │   └── ProductService.application/
│   │   └── external/
│   │       ├── ProductService.api/
│   │       │   └── Consumers/              # MassTransit consumers for events
│   │       └── ProductService.infrastructure/
│   │
└── README.md
```

---

## Prerequisites

### Required Software
- **.NET 10 SDK** ([Download](https://dotnet.microsoft.com/download))
- **SQL Server** (LocalDB or Express)
- **RabbitMQ** (for message broker)
- **Visual Studio 2026** or VS Code with C# extension

### Optional Tools
- **Postman** or **Insomnia** for API testing
- **Azure Data Studio** for database management

---

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/EcommerceApp.git
cd EcommerceApp
```

### 2. Install RabbitMQ

**Option A: Using Docker** (Recommended)
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 \
  -e RABBITMQ_DEFAULT_USER=guest \
  -e RABBITMQ_DEFAULT_PASS=guest \
  rabbitmq:4-management
```

Access RabbitMQ management console at: `http://localhost:15672` (guest/guest)

**Option B: Local Installation**
- Download from [rabbitmq.com](https://www.rabbitmq.com/download.html)
- Follow platform-specific installation instructions

### 3. Configure Databases

Each service uses LocalDB with separate databases:

```
Server=(localdb)\MSSQLLocalDB
Database: IdentityDb
Database: OrderDb
Database: EcommerceDb (ProductService)
```

### 4. Update Connection Strings

Update `appsettings.json` in each service's API project:

**IdentityService/src/external/IdentityService.api/appsettings.json:**
```json
{
  "ConnectionStrings": {
    "AppDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=IdentityDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Secret": "YourSuperSecretKeyChangeInProduction123!",
    "Issuer": "IdentityService",
    "Audience": "EcommerceApp",
    "ExpiryMinutes": 60
  }
}
```

**ProductService/src/external/ProductService.api/appsettings.json:**
```json
{
  "ConnectionStrings": {
    "AppDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EcommerceDb;Trusted_Connection=True;"
  },
  "MessageBroker": {
    "Host": "rabbitmq://localhost",
    "Username": "guest",
    "Password": "guest"
  }
}
```

**OrderService/src/external/OrderService.api/appsettings.json:**
```json
{
  "ConnectionStrings": {
    "AppDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=OrderDb;Trusted_Connection=True;"
  },
  "MessageBroker": {
    "Host": "rabbitmq://localhost",
    "Username": "guest",
    "Password": "guest"
  }
}
```

### 5. Apply Database Migrations

Open Package Manager Console and execute for each service:

```powershell
# IdentityService
Update-Database -Project IdentityService.infrastructure -Startup IdentityService.api

# OrderService
Update-Database -Project OrderService.infrastructure -Startup OrderService.api

# ProductService
Update-Database -Project ProductService.infrastructure -Startup ProductService.api
```

Or use .NET CLI:
```bash
dotnet ef database update --project .\IdentityService\src\external\IdentityService.infrastructure\ -s .\IdentityService\src\external\IdentityService.api\
```

### 6. Build and Run

```bash
# Build solution
dotnet build

# Run each service in separate terminals
dotnet run --project IdentityService/src/external/IdentityService.api/IdentityService.api.csproj
dotnet run --project OrderService/src/external/OrderService.api/OrderService.api.csproj
dotnet run --project ProductService/src/external/ProductService.api/ProductService.api.csproj
```

Services will start on:
- IdentityService: `https://localhost:5001`
- OrderService: `https://localhost:5002`
- ProductService: `https://localhost:5003`

---

## API Endpoints

### IdentityService

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "john_doe",
  "email": "john@example.com",
  "passwordHash": "SecurePassword123!"
}
```

**Response (200 OK):**
```json
{
  "responseCode": 200,
  "responseMessage": "User registered successfully",
  "responseData": {
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "email": "john@example.com"
  }
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "SecurePassword123!"
}
```

**Response (200 OK):**
```json
{
  "responseCode": 200,
  "responseMessage": "Login successful",
  "responseData": {
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

### ProductService

#### Get All Products
```http
GET /api/products
Authorization: Bearer {token}
```

#### Get Product by ID
```http
GET /api/products/{id}
Authorization: Bearer {token}
```

### OrderService

#### Create Order
```http
POST /api/orders
Content-Type: application/json
Authorization: Bearer {token}

{
  "productIds": ["550e8400-e29b-41d4-a716-446655440000"],
  "quantity": 2,
  "customerId": "550e8400-e29b-41d4-a716-446655440001"
}
```

---

## Architectural Decisions

### 1. **Microservices Architecture**
**Decision**: Separated into three independent services
- **Rationale**: Allows independent scaling, deployment, and technology choices
- **Benefit**: Each service can be developed and maintained by different teams
- **Trade-off**: Increased complexity in inter-service communication

### 2. **Clean Architecture + CQRS (IdentityService)**
**Decision**: Organized layers: Domain → Application → Infrastructure → Presentation
- **Rationale**: Clear separation of concerns, testability, and maintainability
- **Benefit**: Easy to understand, modify, and test business logic
- **Pattern**: CQRS with MediatR for command/query separation

### 3. **Database Per Service Pattern**
**Decision**: Each service has its own SQL Server database
- **Rationale**: Ensures service autonomy and prevents tight coupling via shared data
- **Benefit**: Services can evolve their schemas independently
- **Trade-off**: Distributed transactions become complex

### 4. **Event-Driven Communication (RabbitMQ + MassTransit)**
**Decision**: Async messaging instead of synchronous HTTP calls
- **Rationale**: Loose coupling, better fault tolerance, and scalability
- **Implementation**: OrderCreated event published → ProductService consumes and updates stock
- **Benefit**: Services remain operational even if one is temporarily down

### 5. **JWT Authentication**
**Decision**: Stateless token-based authentication
- **Rationale**: Scales well across multiple services without session state
- **Benefit**: Each service can validate tokens independently using the public key
- **Configuration**: Centralized in IdentityService, validated locally

### 6. **BCrypt for Password Hashing**
**Decision**: Industry-standard password hashing
- **Rationale**: Adaptive, slow-by-design algorithm resistant to brute force attacks
- **Benefit**: Better security than simple hashing

### 7. **.NET 10 with Minimal APIs**
**Decision**: Latest .NET framework with modern ASP.NET Core features
- **Rationale**: Latest security patches, performance improvements, and language features
- **Benefit**: Better native AOT compilation, performance enhancements

---

## Trade-offs Made

### 1. **Eventual Consistency vs. Strong Consistency**
| Aspect | Decision | Reason |
|--------|----------|--------|
| **Approach** | Eventual consistency via async events | Improves availability and prevents cascading failures |
| **Trade-off** | Temporary data inconsistency possible | RabbitMQ ensures "at least once" delivery |
| **Impact** | OrderCreatedConsumer updates stock asynchronously | May take seconds, acceptable for e-commerce |

### 2. **Complexity vs. Scalability**
| Aspect | Decision | Reason |
|--------|----------|--------|
| **Approach** | Microservices over monolith | Better independent scaling |
| **Trade-off** | Operational complexity increases | Multiple deployments, monitoring, logging |
| **Mitigation** | Docker, containerization (future) | Simplifies deployment |

### 3. **RabbitMQ Dependency**
| Aspect | Decision | Reason |
|--------|----------|--------|
| **Approach** | External message broker | Standard industry practice |
| **Trade-off** | Additional infrastructure to manage | Enables loose coupling |
| **Failure**: If RabbitMQ down, async events fail | Implement retry logic and DLQ (future) | Ensure reliability |

### 4. **IdentityService as Single Point of Auth**
| Aspect | Decision | Reason |
|--------|----------|--------|
| **Approach** | Centralized authentication | Easier to manage users and permissions |
| **Trade-off** | Creates potential bottleneck | Token validation done locally in each service |
| **Mitigation** | Token-based (stateless) auth | Services don't need to call IdentityService for validation |

### 5. **Database Per Service (No Distributed Transactions)**
| Aspect | Decision | Reason |
|--------|----------|--------|
| **Approach** | Separate databases | Service autonomy |
| **Trade-off** | Complex compensation logic for failed operations | Implement Saga pattern (future) |
| **Example** | Order created but product stock can't update | Retry with exponential backoff |

---

## Future Improvements

### 1. **API Gateway**
```
Client → API Gateway → Services
```
- **Benefits**: Centralized request routing, rate limiting, authentication
- **Implementation**: Use Azure API Management or open-source Kong
- **Priority**: High

### 2. **Service Resilience & Circuit Breaker**
```csharp
// Use Polly for resilience
services.AddHttpClient("OrderService")
    .AddTransientHttpErrorPolicy()
    .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 3);
```
- **Benefits**: Graceful degradation, prevent cascading failures
- **Priority**: High

### 3. **Distributed Tracing & Logging**
- **Implementation**: Azure Application Insights or ELK Stack
- **Benefits**: Monitor service interactions, debug production issues
- **Priority**: High

### 4. **Saga Pattern for Distributed Transactions**
```
Order Created → Reserve Stock → Process Payment → Confirm Order
                ↓ (failure) → Rollback Stock → Refund Payment
```
- **Implementation**: MassTransit Saga with state machine
- **Benefits**: Ensure data consistency across services
- **Priority**: Medium

### 5. **Containerization & Orchestration**
```dockerfile
# Docker containers for each service
# Kubernetes for orchestration
```
- **Benefits**: Consistent deployment, auto-scaling
- **Priority**: Medium

### 6. **Refresh Token Rotation**
- **Current**: Only access tokens implemented
- **Improvement**: Implement refresh token rotation with secure HttpOnly cookies
- **Benefits**: Better security, prevent token theft
- **Priority**: Medium

### 7. **Request Validation at API Gateway**
- **Current**: Validation in handlers
- **Improvement**: Centralize validation, reduce load on services
- **Priority**: Low

### 8. **Caching Layer**
```csharp
services.AddStackExchangeRedisCache(options => 
    options.Configuration = configuration["Redis:ConnectionString"]);
```
- **Benefits**: Reduce database load, faster response times
- **Use Cases**: Product catalog, user profiles
- **Priority**: Low

### 9. **Dead Letter Queue (DLQ) for Failed Messages**
- **Current**: Messages can be lost if processing fails
- **Improvement**: Implement DLQ in RabbitMQ for failed events
- **Benefits**: Easier debugging, message recovery
- **Priority**: Medium

### 10. **Comprehensive Integration Tests**
- **Current**: Limited test coverage
- **Improvement**: Add TestContainers for automated integration testing
- **Benefits**: Catch issues earlier, safer deployments
- **Priority**: High

### 11. **Role-Based Access Control (RBAC)**
```csharp
[Authorize(Roles = "Admin")]
public async Task<IActionResult> DeleteProduct(string id) { }
```
- **Current**: Basic JWT authentication
- **Improvement**: Add roles and permissions
- **Priority**: Medium

### 12. **Audit Logging**
- **Current**: No audit trail
- **Improvement**: Log all sensitive operations (user registration, data modifications)
- **Benefits**: Compliance, security investigation
- **Priority**: Medium

---

## Performance Optimization Opportunities

| Optimization | Current | Proposed | Impact |
|--------------|---------|----------|--------|
| **Database Indexing** | Basic | Index frequently queried fields | High |
| **Query Optimization** | N+1 queries possible | Use EF Core's `.Include()` | High |
| **Caching** | No caching | Redis cache layer | Medium |
| **Async All The Way** | Mostly implemented | Full async end-to-end | Medium |
| **Pagination** | Not implemented | Add pagination to list endpoints | Medium |
| **Response Compression** | Not configured | Enable Gzip compression | Low |

---

## Security Considerations

### Current Implementations
✅ JWT token-based authentication
✅ BCrypt password hashing
✅ HTTPS enforcement
✅ Input validation with FluentValidation

### Recommended Additions
- [ ] Rate limiting (prevent brute force attacks)
- [ ] CORS policy configuration
- [ ] SQL injection prevention (using parameterized queries - already done with EF Core)
- [ ] XSS protection headers
- [ ] API key authentication for service-to-service communication
- [ ] Secrets management (Azure Key Vault)

---

## Deployment

### Local Development
Tested on Windows 11 with LocalDB and RabbitMQ Docker container

### Production Considerations
- Use Azure SQL Database or cloud-managed SQL Server
- Managed RabbitMQ (Azure Service Bus or AWS SQS)
- Use Application Insights for monitoring
- Implement CI/CD with Azure DevOps or GitHub Actions
- Container registry (Docker Hub, Azure Container Registry)

---

## Contributing

1. Follow Clean Architecture principles
2. Use CQRS for complex operations
3. Write unit tests for business logic
4. Add integration tests for critical flows
5. Keep controllers thin (logic in handlers)

---

## Troubleshooting

### RabbitMQ Connection Issues
```bash
# Check if RabbitMQ is running
docker ps | grep rabbitmq

# View RabbitMQ logs
docker logs rabbitmq
```

### Database Migration Errors
```bash
# Remove latest migration
Remove-Migration -Project IdentityService.infrastructure

# Reapply migrations
Update-Database -Project IdentityService.infrastructure
```

### JWT Token Validation Failures
- Ensure `appsettings.json` Jwt:Secret matches across services
- Check token expiration time
- Verify token includes required claims

---

## License

MIT License - See LICENSE file for details

---

## Contact & Support

For questions or issues:
- Create a GitHub Issue
- Email: your-email@example.com

---

**Last Updated**: May 2025 | **.NET 10** | **Clean Architecture + CQRS + Microservices**
