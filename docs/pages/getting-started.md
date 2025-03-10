# Getting Started

## Get the SDK

To get started with .NET, you can download the SDK binaries for your platform from [the official download page](https://dotnet.microsoft.com/en-us/download).

::: info 🙋🏻‍♀️ Do I need Windows to write C#?
Definitely not!  That's a myth and one of the purposes of this guide is to help shed these myths!

I work on C# on macOS full time and the last startup (VC-backed, $8m seed), the entire team worked on M1 MacBook Pros and we shipped to AWS t4g Arm64 instances.  You definitely do not need Windows to work on C#; the experience in VS Code isn't any different from TypeScript.  Just as you would add the TypeScript language extension to VS Code, install the C# language extension as well.
:::

## Get an IDE

There are three primary IDEs depending on which platform you are on:

|IDE|Platforms|Pricing|Great For...|
|--|--|--|--|
|VS Code|Linux, macOS, Windows|Free|Console apps, web APIs, Blazor web apps; get the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) (or the [C# DevKit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) if you prefer some of the visual features around project and solution management similar to Visual Studio)|
|Rider|Linux, macOS, Windows|[Free for non-commercial use](https://www.jetbrains.com/rider/buy/?section=personal&billing=yearly)|All .NET project types, devs who like JetBrains IDEs, devs who need the BEST refactoring tools.|
|Visual Studio|Windows|[Free community license](https://visualstudio.microsoft.com/downloads/)|All .NET project types|

Of course, [you can also use `vim`](https://github.com/OmniSharp/omnisharp-vim)!

If you don't want to install the SDK, then just start with the **Polyglot Notebooks extension** which will let you try C# using C# interactive (see below).

::: tip
In general, I find that VS Code with the C# or DevKit extension to be perfectly fine even for large .NET codebases.  I have used it with a 100,000 line codebase at a startup without issues.  I strongly recommend starting there since you are probably already using it for your TypeScript work!

Where Jetbrains Rider shines is when you need to do complex refactorings.
:::

## Get .NET Interactive and Polyglot Notebooks

[VS Code Polyglot Notebooks](https://code.visualstudio.com/docs/languages/polyglot) (available via [this extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode)) are a great way to experiment with C# without setting up a runtime infrastructure.

This is a great way to get started and experiment and, in fact, you'll find a notebook in this repo in [`src/csharp/csharp-notebook.dib`](https://github.com/CharlieDigital/typescript-is-like-csharp/blob/main/src/csharp/csharp-notebook.dib)!
