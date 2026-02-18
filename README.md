# Depsinc

> Incremental dependency injection source generator for .NET.

## Description
Depsinc is a .NET source generator that automates dependency injection registration using incremental Roslyn generators. Annotate your services with attributes and Depsinc generates the `IServiceCollection` extension methods at compile time, eliminating manual DI wiring.

## Features
- Incremental Roslyn source generator for zero compile-time overhead
- Attribute-based service registration
- Generates `IServiceCollection` extension methods automatically
- Supports scoped, transient, and singleton lifetimes

## Getting Started
```bash
dotnet add package phmatray.Depsinc
```

```csharp
[Singleton]
public class MyService : IMyService { }

// In Program.cs — generated automatically:
// builder.Services.AddDepsinc();
```

## License
MIT