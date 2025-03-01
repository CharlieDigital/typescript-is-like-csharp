# Getting Started

## Get the SDK

To get started with .NET, you can download the SDK binaries for your platform from [the official download page](https://dotnet.microsoft.com/en-us/download).

## Get an IDE

There are three primary IDEs depending on which platform you are on:

|IDE|Platforms|Pricing|Great For...|
|--|--|--|--|
|VS Code|Linux, macOS, Windows|Free|Console apps, web APIs; get the [C# DevKit extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)|
|Rider|Linux, macOS, Windows|[Free for non-commercial use](https://www.jetbrains.com/rider/buy/?section=personal&billing=yearly)|All .NET project types, devs who like JetBrains IDEs, devs who need the BEST refactoring tools.|
|Visual Studio|Windows|[Free community license](https://visualstudio.microsoft.com/downloads/)|All .NET project types|

Of course, [you can also use `vim`](https://github.com/OmniSharp/omnisharp-vim)!

> In general, I find that VS Code with the C# DevKit to be perfectly fine even for large .NET codebases.  I have used it with a 100,000 line codebase at a startup without issues.  I strongly recommend starting there since you are probably already using it for your TypeScript work!
>
> Where Jetbrains Rider shines is when you need to do complex refactorings.

## Get .NET Interactive and Polyglot Notebooks

[VS Code Polyglot Notebooks](https://code.visualstudio.com/docs/languages/polyglot) (available via [this extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.dotnet-interactive-vscode)) are a great way to experiment with C# without setting up a runtime infrastructure.

This is a great way to get started and experiment and, in fact, you'll find a notebook in this repo in [`src/csharp/csharp-notebook.dib`](https://github.com/CharlieDigital/typescript-is-like-csharp/blob/main/src/csharp/csharp-notebook.dib)!
