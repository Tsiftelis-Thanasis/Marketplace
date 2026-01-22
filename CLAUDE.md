# Claude Project Guidelines

## Project Overview
This is an ASP.NET application using Redis for caching and session management. The project follows modern .NET best practices with clean architecture principles.

## Technology Stack
- **Framework**: ASP.NET Core (specify version, e.g., .NET 8.0)
- **Caching**: Redis
- **Database**: (specify: SQL Server, PostgreSQL, etc.)
- **ORM**: Entity Framework Core
- **API Style**: RESTful API / Minimal APIs
- **Authentication**: JWT / Identity / (specify your auth method)

## Code Standards

### C# Style Guidelines
- Follow Microsoft C# Coding Conventions
- Use PascalCase for class names, method names, and properties
- Use camelCase for local variables and parameters
- Use async/await for all I/O operations
- Maximum line length: 120 characters
- Use nullable reference types throughout
- Use file-scoped namespaces (namespace MyProject.Services;)

### Project Structure
```
/src
  /YourProject.API          # Web API / MVC Controllers
  /YourProject.Application  # Business logic, services, DTOs
  /YourProject.Domain       # Entities, interfaces, domain models
  /YourProject.Infrastructure # Data access, external services, Redis
/tests
  /YourProject.UnitTests
  /YourProject.IntegrationTests
```

### Naming Conventions
- Controllers: `[Entity]Controller.cs` (e.g., `UsersController.cs`)
- Services: `I[Name]Service.cs` and `[Name]Service.cs`
- Repositories: `I[Entity]Repository.cs` and `[Entity]Repository.cs`
- DTOs: `[Entity][Action]Dto.cs` (e.g., `UserCreateDto.cs`, `UserResponseDto.cs`)
- Redis Keys: Use consistent prefixes (e.g., `user:{userId}`, `session:{sessionId}`)

## Redis Usage Guidelines

### Connection Management
- Use `IConnectionMultiplexer` from StackExchange.Redis
- Register as singleton in dependency injection
- Connection string should be in appsettings.json or environment variables
- Never hardcode Redis connection strings

### Caching Strategy
- Use Redis for:
  - Session data
  - Frequently accessed data (user profiles, settings)
  - API response caching
  - Rate limiting counters
  - Distributed locks
- Cache expiration: Set appropriate TTL for all cached items
- Key naming: Use hierarchical keys with colons (e.g., `cache:users:{id}`, `session:{sessionId}`)

### Redis Best Practices
- Always set expiration times to prevent memory bloat
- Use pipeline for multiple operations
- Implement cache-aside pattern for data access
- Handle Redis connection failures gracefully
- Log cache misses for monitoring

## Dependency Injection

### Service Registration
- Register services in `Program.cs` or use extension methods
- Use appropriate lifetimes:
  - Singleton: Redis connection, configuration
  - Scoped: DbContext, repositories, business services
  - Transient: Lightweight, stateless services

### Example Service Registration Pattern
```csharp
// In ConfigureServices or Program.cs
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
```

## Testing Requirements

### Unit Tests
- Test framework: xUnit / NUnit
- Mocking: Moq or NSubstitute
- Minimum coverage: 80% for business logic
- Mock external dependencies (Redis, database, HTTP clients)
- Name pattern: `[MethodName]_[Scenario]_[ExpectedResult]`

### Integration Tests
- Use WebApplicationFactory for API testing
- Use Testcontainers for Redis and database
- Clean up test data after each test
- Test Redis operations with actual Redis instance in Docker

### Example Test Structure
```csharp
public class UserServiceTests
{
    [Fact]
    public async Task GetUserById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        // Act
        // Assert
    }
}
```

## Configuration Management

### appsettings.json Structure
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;",
    "Redis": "localhost:6379"
  },
  "Redis": {
    "InstanceName": "MyApp:",
    "DefaultExpirationMinutes": 60
  },
  "Jwt": {
    "SecretKey": "use-environment-variables",
    "Issuer": "your-app",
    "Audience": "your-app-users",
    "ExpirationMinutes": 60
  }
}
```

### Environment-Specific Settings
- Use appsettings.Development.json for local settings
- Use appsettings.Production.json for production
- Never commit secrets - use User Secrets for local dev
- Use environment variables or Azure Key Vault for production secrets

## API Design Standards

### RESTful Conventions
- Use HTTP verbs correctly: GET, POST, PUT, DELETE, PATCH
- Return appropriate status codes:
  - 200 OK for successful GET
  - 201 Created for successful POST
  - 204 No Content for successful DELETE
  - 400 Bad Request for validation errors
  - 401 Unauthorized for auth failures
  - 404 Not Found for missing resources
  - 500 Internal Server Error for unexpected errors

### Request/Response Patterns
- Use DTOs for all API inputs and outputs
- Never expose domain entities directly
- Return consistent error response format:
```csharp
{
    "error": "Error message",
    "details": ["Validation error 1", "Validation error 2"],
    "traceId": "correlation-id"
}
```

### Validation
- Use Data Annotations or FluentValidation
- Validate at the controller level
- Return 400 with validation details
- Example: `[Required]`, `[StringLength(100)]`, `[EmailAddress]`

## Middleware and Filters

### Required Middleware Order
1. Exception handling
2. HTTPS redirection
3. Static files
4. Routing
5. Authentication
6. Authorization
7. CORS (if needed)
8. Custom middleware
9. Endpoint mapping

### Exception Handling
- Use global exception handler middleware
- Log all exceptions with correlation IDs
- Never expose stack traces in production
- Return user-friendly error messages

## Logging

### Logging Standards
- Use ILogger<T> from Microsoft.Extensions.Logging
- Log levels:
  - Trace: Detailed diagnostic info
  - Debug: Development debugging
  - Information: General flow (requests, cache hits)
  - Warning: Unexpected but handled (cache misses, retries)
  - Error: Errors and exceptions
  - Critical: Application crashes
- Include structured logging with contextual data
- Log Redis operations (cache hits/misses)

### What to Log
- All API requests (method, path, status code, duration)
- Redis operations (hits, misses, errors)
- Authentication attempts
- Database query performance issues
- External API calls
- Exceptions with full details

## Security Best Practices

### Authentication & Authorization
- Use JWT tokens with appropriate expiration
- Store tokens securely (httpOnly cookies or secure storage)
- Implement refresh token mechanism
- Use [Authorize] attribute on protected endpoints
- Implement role-based or claims-based authorization

### Data Protection
- Never log sensitive data (passwords, tokens, PII)
- Use HTTPS in production
- Implement rate limiting (use Redis for counters)
- Sanitize user inputs
- Use parameterized queries (EF Core handles this)
- Enable CORS only for trusted origins

### Secrets Management
- Use User Secrets for local development
- Use Azure Key Vault or similar for production
- Never commit appsettings.Production.json with secrets
- Rotate API keys and connection strings regularly

## Performance Optimization

### Caching Strategy
- Cache frequently accessed, rarely changing data
- Use Redis for distributed caching across instances
- Implement cache warming for critical data
- Set appropriate TTL based on data volatility
- Monitor cache hit ratios

### Database Optimization
- Use async methods for all database operations
- Implement pagination for large datasets
- Use `AsNoTracking()` for read-only queries
- Add indexes for frequently queried columns
- Use projection (Select) to fetch only needed columns

### Redis Optimization
- Use pipeline for bulk operations
- Implement connection pooling (handled by ConnectionMultiplexer)
- Use appropriate data structures (strings, hashes, sets, sorted sets)
- Monitor Redis memory usage

## Git Workflow

### Branch Naming
- Feature branches: `feature/add-user-authentication`
- Bug fixes: `fix/redis-connection-timeout`
- Hotfixes: `hotfix/critical-security-patch`
- Releases: `release/v1.2.0`

### Commit Messages
Use Conventional Commits format:
- `feat: add user registration endpoint`
- `fix: resolve Redis connection pooling issue`
- `refactor: extract caching logic to separate service`
- `test: add integration tests for authentication`
- `docs: update API documentation`
- `chore: update NuGet packages`

### Pull Request Requirements
- All tests must pass
- Code coverage must not decrease
- No merge conflicts
- At least one approval (if team workflow)
- Update documentation if API changes

## Common Implementation Patterns

### Implementing a New API Endpoint
1. Create/update controller in `/API/Controllers`
2. Create request/response DTOs in `/Application/DTOs`
3. Implement service interface and implementation in `/Application/Services`
4. Add repository if database access needed in `/Infrastructure/Repositories`
5. Add Redis caching if appropriate
6. Write unit tests for service logic
7. Write integration tests for the endpoint
8. Update API documentation

### Adding Redis Caching to a Service
1. Inject `IConnectionMultiplexer` or `IDistributedCache`
2. Implement cache-aside pattern:
   - Check cache first
   - If miss, fetch from database
   - Store in cache with TTL
   - Return data
3. Add cache invalidation logic when data changes
4. Add logging for cache operations

### Creating a New Entity
1. Create domain entity in `/Domain/Entities`
2. Add DbSet to DbContext
3. Create migration: `dotnet ef migrations add AddEntityName`
4. Create repository interface and implementation
5. Create service for business logic
6. Create DTOs for API requests/responses
7. Add controller endpoints
8. Write comprehensive tests

## What NOT to Do

- Don't commit connection strings or API keys
- Don't use `Task.Result` or `.Wait()` - always use `await`
- Don't catch exceptions without logging them
- Don't expose internal exception details to API clients
- Don't use `String.Format` - use string interpolation or `StringBuilder`
- Don't create new Redis connections per request
- Don't cache everything - be selective
- Don't skip migrations - always use EF migrations
- Don't use `SELECT *` in queries
- Don't ignore compiler warnings
- Don't skip writing tests for new features
- Don't modify entities directly from controllers

## Development Workflow

### Local Development Setup
1. Install .NET SDK (version X.X)
2. Install Redis (via Docker or local install)
3. Install SQL Server / PostgreSQL
4. Clone repository
5. Run: `dotnet restore`
6. Update appsettings.Development.json with local connection strings
7. Run migrations: `dotnet ef database update`
8. Run: `dotnet run`

### Before Committing
1. Run all tests: `dotnet test`
2. Check code formatting
3. Remove unused using statements
4. Ensure no commented-out code
5. Update documentation if needed

## Useful Commands

```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run tests
dotnet test

# Run with watch (auto-reload)
dotnet watch run

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Generate API client / OpenAPI spec
dotnet swagger tofile --output swagger.json bin/Debug/net8.0/YourProject.API.dll v1
```

## Additional Notes

- Prefer async/await over synchronous code
- Use record types for DTOs when appropriate (C# 9+)
- Implement health checks for Redis, database, and external dependencies
- Use Background Services for long-running tasks
- Consider implementing CQRS for complex business logic
- Monitor application performance and Redis metrics in production
