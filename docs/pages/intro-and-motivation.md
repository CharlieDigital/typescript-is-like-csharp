# Intro and Motivation

Many teams find themselves outgrowing Node.js and TypeScript on the backend when building systems of consequence.  In particular, TypeScript helps at dev and build time, but of course it's just JavaScript at runtime with all of its pitfalls and potential issues due to the lack of a strong static type system.

Teams will often consider alternatives such as:

- Go
- Java
- Kotlin
- Scala
- Rust (?!?)

But many teams end up overlooking .NET and C#. Some teams may even have an inherent bias against C# because of its "Micro$oft" roots *while fully embracing other Microsoft products like GitHub, TypeScript, and VS Code!* (the irony)

In fact, C# is probably the most natural choice for teams that are already adept at TypeScript because the languages share so much in common.  ***Both TypeScript and C# are designed by [Anders Hejlsberg](https://en.wikipedia.org/wiki/Anders_Hejlsberg) of Microsoft***.  Because of this lineage and the long influence of C# on JavaScript and vice versa, C# and TypeScript have actually been converging over the last decade.

Many developers, engineering managers, and CTOs may have come across C# and .***NET Framework*** at some point in their career, but haven't looked at it since Microsoft pivoted to the open source ***.NET Core*** initiative (that yielded the open source, numbered .NET versions).  Today's .NET is very different from *.NET Framework* of the 2000's.  Let's start with a simple distinction:

- `.NET Framework` - Win32 bindings, legacy, and primarily maintained supported for enterprise use cases.  You'll see this as `.NET Framework 4.8.x`
- `.NET 5`, `.NET 6`, `.NET 7`, `.NET 8`, `.NET 9`, etc - Modern, cross-platform .NET that runs on Linux, macOS, and Windows with x64 or Arm64 bindings.
- `.NET Core`, `.NET Standard` - Designator of the bifurcation point between legacy and modern (long legacy of terrible nomenclature from Microsoft...)

> I often wonder if Microsoft would have been better off just calling it `dot` instead when they made the transition from .NET Framework...

![Image comparing JS/TS/C#](../assets/js-ts-csharp.png)

## Why C#

### Strongly Typed Yet *Flexible*

It turns out that -- if you are doing anything of consequence -- you actually probably want to have strong, static types on your backend because this will help reduce mistakes and errors while also reducing verbosity in the overall codebase required in JS to ensure some level of type safety (e.g. Zod schemas).

But .NET has a few interesting tricks up its sleeve when it comes to types such as:

- Type inference
- Tuples
- Named tuples
- Anonymous types
- Type extensions
- Union types (via 3rd party extensions [for now...](https://github.com/dotnet/csharplang/blob/main/proposals/TypeUnions.md))

### Performance + DX

- C# is not as fast and memory efficient as Rust
- Because it's JIT, it compiles to larger binaries than Go
- It also means it starts up slower than JavaScript

But for all of that, .NET still performs extremely well across the board in a variety of applications and with [ahead-of-time compilation (AOT)](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/?tabs=windows%2Cnet8), it is possible to achieve relatively fast startup times.

For backends, .NET offers high performance roughly in the same ballpark with Java and Go with an excellent DX because of the large standard library and first party packages from Microsoft ("batteries included").

### Tooling

.NET offers great tooling support in VS Code (free) and Rider (free personal, paid commercial) and is support on Linux, macOS, and of course Windows.  It supports x64 and Arm64 architectures.

The [dotnet cli](https://learn.microsoft.com/en-us/dotnet/core/tools/) is easy to use.

### Documentation

Microsoft's [excellent documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/) is cohesive and thorough, making it easy to read and understand.  In Node, you might read documentation across a variety of third party sites for different packages (Zod, Prisma, Nest.js) which have different levels of thoroughness.  .NET's large standard library means that much of it also has first party documentation.

Perhaps more importantly, it means that you'll likely get better results with LLMs because of the high quality documentation.

### Security

JavaScript's lack of a standard library means that a lot of functionality depends on third parties.  On the other hand, .NET's large first party ecosystem means that if there are security issues, they will get patched.  With third party libraries, this is a roll of the dice!

Historically, NPM has also been a known vector for supply chain attacks and JavaScript's nature as a dynamic language means that it is susceptible to types of attacks that are not possible in a .NET runtime.

Check out [GitHub's 2020 State of the Octoverse Security report for more insights](https://octoverse.github.com/2020/).

### Mature but Constantly Evolving

C# and .NET are constantly evolving and the language consistently improves DX with each release, reducing the verbosity of the language.  It's akin to the relationship between Kotlin and Java on the JVM, except C# has continued to rapidly evolve as a language.

Check out some of the latest releases:

- [C# 13](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13)
- [C# 12](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12)
- [C# 11](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11)
- [C# 10](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10)

What should be clear is how much of a focus the .NET team has a ergonomics and DX.

## Common Myths

### C# Requires Visual Studio and Expensive Licenses

This is not the case; in fact C# works great from VS Code!  It's completely free to use and you can use the C# extensions without a license.

### .NET is Windows Only

This was true of .NET Framework which had bindings to the Win32 dlls.  Since .NET Core, Microsoft has been making .NET truly cross platform and any of the "numbered versions" like .NET 6, 7, 8, 9 can all run on Linux, macOS, and Windows.

In fact, at one startup, we developed .NET on M1 MacBook Pros, build our containers in GitHub Actions, and ran them in AWS on Arm64 `t4g` instances.

.NET is a great cross-platform backend.

### .NET is a Legacy Platform

This is true of ***.NET Framework***, but the numbered .NET versions (.NET 6, 7, 8, 9) are modern and the underlying platform evolves extremely fast and is perhaps more akin to Kotlin in that sense.

### .NET is Hard to Learn

That's why this doc exists 😅 In fact, .NET's congruence with TypeScript means that developers that know JavaScript and TypeScript should be able to easily transition to C#.
