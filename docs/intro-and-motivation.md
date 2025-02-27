# Intro and Motivation

Many teams find themselves outgrowing TypeScript on the backend when building systems of consequence.  Teams will often consider alternatives such as:

- Go
- Java
- Kotlin
- Rust (?!?)

But many teams end up overlooking .NET and C#, even though both TypeScript and C# are designed by [Anders Hejlsberg](https://en.wikipedia.org/wiki/Anders_Hejlsberg) of Microsoft.  Because of this lineage and the long influence of C# on JavaScript and vice versa, C# and TypeScript have actually been converging over the last decade.

In fact, C# is probably the most natural choice for teams that are already adept at TypeScript because the languages share so much in common.

## Why C#

### Performance + DX

- C# is not as fast and memory efficient as Rust
- Because it's JIT, it compiles to larger binaries than Go
- It also means it starts up slower than JavaScript

But for all of that, .NET still performs extremely well across the board in a variety of applications and with [ahead-of-time compilation (AOT)](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=windows%2Cnet8), it is possible to achieve relatively fast startup times.

For backends, .NET offers high performance roughly in the same ballpark with Java and Go with an excellent DX because of the large standard library and first party packages from Microsoft ("batteries included").

### Tooling and Documentation

### Security

### Mature but Constantly Evolving

## Common Myths

### C# Requires Visual Studio

This is not the case; in fact C# works great from VS Code!  It's completely free to use and you can use the C# extensions without a license.

### .NET is Windows Only

This was true of .NET Framework which had bindings to the Win32 dlls.  Since .NET Core, Microsoft has been making .NET truly cross platform and any of the "numbered versions" like .NET 6, 7, 8, 9 can all run on Linux, macOS, and Windows.

In fact, at one startup, we developed .NET on M1 MacBook Pros, build our containers in GitHub Actions, and ran them in AWS on Arm64 `t4g` instances.

.NET is a great cross-platform backend.

### .NET is a Legacy Platform

This is true of .NET *Framework*, but the numbered .NET versions (.NET 6, 7, 8, 9) are modern and the underlying platform evolves extremely fast and is perhaps more akin to Kotlin in that sense.

### .NET is Hard to Learn

That's why this doc exists 😅 In fact, .NET's congruence with TypeScript means that developers that know JavaScript and TypeScript should be able to easily transition to C#.
