![CqrsHelpers banner](.github/banner.png)

# CqrsHelpers

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - CqrsHelpers](https://img.shields.io/static/v1?label=phmatray&message=CqrsHelpers&color=blue&logo=github)](https://github.com/phmatray/CqrsHelpers)
![Top language](https://img.shields.io/github/languages/top/phmatray/CqrsHelpers)
[![Stars](https://img.shields.io/github/stars/phmatray/CqrsHelpers?style=social)](https://github.com/phmatray/CqrsHelpers/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/CqrsHelpers?style=social)](https://github.com/phmatray/CqrsHelpers/network/members)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/CqrsHelpers)](https://github.com/phmatray/CqrsHelpers/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/CqrsHelpers)](https://github.com/phmatray/CqrsHelpers/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/CqrsHelpers)](https://github.com/phmatray/CqrsHelpers/commits)
<!-- portfolio-badges:end -->

<!-- portfolio-toc:start -->

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Pipeline Behaviors](#pipeline-behaviors)
- [Advanced Usage](#advanced-usage)
- [Why CqrsHelpers?](#why-cqrshelpers)
- [Tech Stack](#tech-stack)
- [License](#license)
- [Contributing](#contributing)

<!-- portfolio-toc:end -->



A lightweight CQRS (Command Query Responsibility Segregation) library for .NET, focused on simplicity and extensibility.

## Features

- **Clean CQRS Pattern**: Separate commands and queries with strongly-typed interfaces
- **Pipeline Behaviors**: Add cross-cutting concerns like validation, logging, and caching
- **Dependency Injection**: Full integration with Microsoft.Extensions.DependencyInjection
- **Type Safety**: Compile-time checking for command/query handlers
- **Minimal Dependencies**: Only depends on Microsoft.Extensions.DependencyInjection.Abstractions

## Installation

```bash
dotnet add package CqrsHelpers
```

## Quick Start

### 1. Define Commands and Queries

```csharp
// Commands modify state
public record CreateUserCommand : ICommand<int>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}

// Queries read state
public record GetUserQuery : IQuery<User>
{
    public required int Id { get; init; }
}
```

### 2. Implement Handlers

```csharp
public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _repository;
    
    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<int> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new User(command.Name, command.Email);
        return await _repository.AddAsync(user, cancellationToken);
    }
}

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    private readonly IUserRepository _repository;
    
    public GetUserQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<User> HandleAsync(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(query.Id, cancellationToken);
    }
}
```

### 3. Configure Services

```csharp
services.AddCqrsHelpers(typeof(Program).Assembly);
```

### 4. Use the Dispatcher

```csharp
public class UserController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    
    public UserController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var userId = await _dispatcher.SendAsync(command);
        return Ok(userId);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _dispatcher.QueryAsync(new GetUserQuery { Id = id });
        return Ok(user);
    }
}
```

## Pipeline Behaviors

Add cross-cutting concerns using pipeline behaviors:

### Validation Example

```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;
    
    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    public async Task<TResponse> HandleAsync(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        return await next();
    }
}
```

### Logging Example

```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> HandleAsync(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling {RequestType}", typeof(TRequest).Name);
        
        var response = await next();
        
        _logger.LogInformation("Handled {RequestType}", typeof(TRequest).Name);
        
        return response;
    }
}
```

## Advanced Usage

### Commands without Return Values

```csharp
public record DeleteUserCommand : ICommand
{
    public required int Id { get; init; }
}

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    public async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        // Delete logic here
    }
}
```

### Registering Handlers from Multiple Assemblies

```csharp
services.AddCqrsHelpers(
    typeof(Domain.AssemblyMarker).Assembly,
    typeof(Application.AssemblyMarker).Assembly
);
```

## Why CqrsHelpers?

- **Simple**: No complex abstractions or overwhelming features
- **Focused**: Built specifically for CQRS, not trying to be everything
- **Extensible**: Easy to add your own behaviors and customizations
- **Testable**: Clean interfaces make unit testing straightforward
- **Performance**: Minimal overhead, behaviors are resolved once per request

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 10**
- xunit.v3
- xunit.runner.visualstudio
- Shouldly
- FakeItEasy
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.DependencyInjection.Abstractions

<!-- portfolio-techstack:end -->

## License

MIT

---

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

<!-- portfolio-sections:end -->
