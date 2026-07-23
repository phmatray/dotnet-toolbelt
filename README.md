![Depsinc banner](.github/banner.png)

# Depsinc

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - Depsinc](https://img.shields.io/static/v1?label=phmatray&message=Depsinc&color=blue&logo=github)](https://github.com/phmatray/Depsinc)
![Top language](https://img.shields.io/github/languages/top/phmatray/Depsinc)
[![Stars](https://img.shields.io/github/stars/phmatray/Depsinc?style=social)](https://github.com/phmatray/Depsinc/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/Depsinc?style=social)](https://github.com/phmatray/Depsinc/network/members)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/Depsinc)](https://github.com/phmatray/Depsinc/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/Depsinc)](https://github.com/phmatray/Depsinc/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/Depsinc)](https://github.com/phmatray/Depsinc/commits)
<!-- portfolio-badges:end -->

<!-- portfolio-toc:start -->

## Table of Contents

- [Description](#description)
- [Features](#features)
- [Getting Started](#getting-started)
- [Tech Stack](#tech-stack)
- [License](#license)
- [Contributing](#contributing)

<!-- portfolio-toc:end -->



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

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 8**
- Shouldly
- FluentAssertions.Analyzers
- Microsoft.Extensions.DependencyInjection
- xunit
- xunit.runner.visualstudio
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
