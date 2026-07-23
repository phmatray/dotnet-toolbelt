# .NET Toolbelt

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
