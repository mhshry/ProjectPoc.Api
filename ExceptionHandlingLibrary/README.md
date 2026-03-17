# ExceptionHandlingLibrary

A comprehensive NuGet package library for exception handling, logging, and middleware/filter components for ASP.NET Core applications.

## Features

### 🔴 Exception Handling
- **CustomException**: Base exception class with error codes and timestamps
- **ValidationException**: For validation errors
- **ResourceNotFoundException**: For resource not found scenarios
- **UnauthorizedException**: For authorization failures
- **ExceptionResult**: Standardized exception response model

### 📝 Logging
- **LogEntry**: Log entry model with metadata
- **LogLevel**: Enumeration for log levels
- **ILogger**: Logger interface abstraction
- **LoggerFactory**: Factory for creating logger instances
- **ConsoleLogger**: Default console logger implementation

### 🔗 Middleware
- **IMiddleware**: Middleware interface contract
- **MiddlewareContext**: Context for passing data between middleware components
- **ErrorHandlingMiddleware**: Global exception handling middleware
- **LoggingMiddleware**: Request/response logging middleware

### 🎯 Filters
- **IFilter**: Filter interface abstraction
- **ExceptionFilter**: Action filter for exception handling
- **LoggingFilter**: Action filter for method logging

## Installation

Add the library to your project by adding a project reference.

## Usage

### 1. Configure Services in Startup

```csharp
services.AddExceptionHandlingLibrary()
    .AddControllers()
    .AddExceptionHandlingFilters();
```

### 2. Add Middleware

```csharp
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
```

### 3. Using Exception Handling

```csharp
[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<TestDto> Get(int id)
    {
        if (id <= 0)
            throw new ValidationException("ID must be greater than 0");

        return Ok(resource);
    }
}
```

## Project Structure

```
ExceptionHandlingLibrary/
├── Exception/
│   ├── CustomException.cs
│   └── ExceptionResult.cs
├── Logging/
│   ├── ILogger.cs
│   ├── LoggerFactory.cs
│   └── LogEntry.cs
├── Middleware/
│   ├── IMiddleware.cs
│   ├── ErrorHandlingMiddleware.cs
│   └── LoggingMiddleware.cs
├── Filters/
│   ├── IFilter.cs
│   ├── ExceptionFilter.cs
│   └── LoggingFilter.cs
└── Extensions/
    └── ServiceCollectionExtensions.cs
```

## License

MIT

## Author

Mahesh Arya