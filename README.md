![LinqPlus banner](.github/banner.png)

﻿# LinqNotNull

LinqNotNull is a lightweight .NET library that extends LINQ to provide additional functionality for handling nullable types in a clean and efficient manner.

[![Nuget](https://img.shields.io/nuget/v/LinqNotNull)](https://www.nuget.org/packages/LinqNotNull/)
[![Build Status](https://img.shields.io/azure-devops/build/{your_org}/{your_project}/{build_definition_id})](https://dev.azure.com/{your_org}/{your_project}/_build/latest?definitionId={build_definition_id}&branchName=main)
[![Test Coverage](https://img.shields.io/azure-devops/coverage/{your_org}/{your_project}/{build_definition_id})](https://dev.azure.com/{your_org}/{your_project}/_build/latest?definitionId={build_definition_id}&branchName=main)

## 📖 Table of Contents
- [Motivation](#-motivation)
- [Getting Started](#-getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
- [Usage](#-usage)
    - [WhereNotNull](#wherenotnull)
    - [SelectNotNull](#selectnotnull)
- [Testing](#-testing)
- [Contributing](#-contributing)
- [License](#-license)
- [Contact](#-contact)
- [FAQ](#-faq)

## 📚 Motivation

The introduction of nullable reference types in C# 8.0 has provided developers with a way to express whether a variable, parameter, or result can be null. This feature has been instrumental in reducing the occurrence of NullReferenceExceptions, a common source of bugs in .NET programs. However, dealing with nullable types can add complexity to LINQ queries. This library aims to simplify the handling of nullable types in LINQ, making your code more readable and maintainable.

## 🚀 Getting Started

### Prerequisites

To use LinqNotNull, you need:
- .NET 6.0, .NET 7.0, or later

### Installation

This library is available as a NuGet package. To install it, use the following command in the NuGet Package Manager Console:

```sh
Install-Package LinqNotNull
```

Or, add this line to your .csproj file:

```xml
<PackageReference Include="LinqNotNull" Version="1.0.0" />
```

## 🔍 Usage
LinqNotNull offers two main extension methods: `WhereNotNull` and `SelectNotNull`.

### WhereNotNull

This method filters a sequence of values based on a predicate and removes null values.

```csharp
IEnumerable<TSource> WhereNotNull<TSource>(
    this IEnumerable<TSource?> source)
```

Example:

```csharp
List<string?> names = new List<string?> { "Alice", null, "Bob", null, "Charlie" };
List<string> filteredNames = names.WhereNotNull().ToList();
// Returns: ["Alice", "Bob", "Charlie"]
```

### SelectNotNull

This method projects each element of a sequence into a new form and removes null values.

```csharp
IEnumerable<TResult> SelectNotNull<TSource, TResult>(
    this IEnumerable<TSource> source,
    Func<TSource, TResult?> selector)
```

Example:

```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
List<string> oddNumbers = numbers.SelectNotNull(n => n % 2 == 0 ? null : n.ToString()).ToList();
// Returns: ["1", "3", "5"]
```

To learn more about nullability and why it's important in modern C# code, check out Microsoft's documentation on [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references).

## 🧪 Testing

LinqNotNull is thoroughly tested to ensure that it works as expected under different scenarios. The tests cover all main features of the library. To run the tests, you will need a test runner compatible with xUnit.net.

If you are contributing to the project, please make sure to add tests for new features or changes to existing ones to help maintain the quality and reliability of the library.

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 6**
- Shouldly
- xunit
- xunit.runner.visualstudio

<!-- portfolio-techstack:end -->

<!-- portfolio-roadmap:start -->

## Roadmap

Planned work and known limitations are tracked in the [open issues](https://github.com/phmatray/LinqPlus/issues). Contributions toward them are welcome.

<!-- portfolio-roadmap:end -->

## 🛠️ Contributing

We welcome contributions! Please see [CONTRIBUTING.md](./CONTRIBUTING.md) for details on how to contribute to LinqNotNull.

## 📜 License

LinqNotNull is licensed under the MIT License. See [LICENSE](./LICENCE.md) for more information.

## 📞 Contact

If you have any questions, please open an issue or submit a pull request.

## ❓ FAQ
If you encounter any problems or have some questions, please check out our [FAQ](./FAQ.md) or open an issue.