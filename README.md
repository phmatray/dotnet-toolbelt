# .NET Toolbelt

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - dotnet-toolbelt](https://img.shields.io/static/v1?label=phmatray&message=dotnet-toolbelt&color=blue&logo=github)](https://github.com/phmatray/dotnet-toolbelt)
![Top language](https://img.shields.io/github/languages/top/phmatray/dotnet-toolbelt)
[![Stars](https://img.shields.io/github/stars/phmatray/dotnet-toolbelt?style=social)](https://github.com/phmatray/dotnet-toolbelt/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/dotnet-toolbelt?style=social)](https://github.com/phmatray/dotnet-toolbelt/network/members)
[![License](https://img.shields.io/github/license/phmatray/dotnet-toolbelt)](https://github.com/phmatray/dotnet-toolbelt/blob/HEAD/LICENSE)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/dotnet-toolbelt)](https://github.com/phmatray/dotnet-toolbelt/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/dotnet-toolbelt)](https://github.com/phmatray/dotnet-toolbelt/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/dotnet-toolbelt)](https://github.com/phmatray/dotnet-toolbelt/commits)
<!-- portfolio-badges:end -->


> A collection of small, focused **.NET utility libraries** — each ships as its own
> NuGet package. Take only what you need.

Consolidated from separate repositories (full git history preserved). Package IDs
are unchanged, so existing installs keep working.

## Packages

| Package | What it does | From |
|---|---|---|
| [`src/throwif`](src/throwif) | **Fluent guard clauses** — throw in one expressive line with auto-captured argument names | `phmatray/CCross.ThrowIf` ★ |
| [`src/record-equality`](src/record-equality) | **True value equality** for records via `ValueCollection<T>` | `phmatray/RecordEquality` |
| [`src/linqplus`](src/linqplus) | **Null-safe LINQ** — `WhereNotNull`, `SelectNotNull`, cleaner nullable handling | `phmatray/LinqPlus` |
| [`src/monads`](src/monads) | **Monads in C#** — practical Option/Result-style types | `phmatray/Monads` |
| [`src/cqrs`](src/cqrs) | **Lightweight CQRS** — pipeline behaviors, DI, type-safe command/query handling | `phmatray/CqrsHelpers` |
| [`src/depsinc`](src/depsinc) | **DI source generator** — automatic dependency-injection registration | `phmatray/Depsinc` |
| [`src/simple-helpers`](src/simple-helpers) | **General helpers** — small everyday .NET utilities | `phmatray/simple-helpers` |
| [`src/ccross-helper`](src/ccross-helper) | **Cross-platform helper classes** to accelerate development | `phmatray/ccross-helper` |

## Related (kept separate)

- **[MasterCommander](https://github.com/phmatray/MasterCommander)** ★ — a tool for driving CLI tools (git, dotnet…) from your code. A tool, not a helper library, so it stays on its own.

## History

Each package was merged with **full git history preserved** (`git subtree`). The
original repositories are archived and redirect here.

## License

MIT — see [`LICENSE`](LICENSE).

---

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

<!-- portfolio-sections:end -->
