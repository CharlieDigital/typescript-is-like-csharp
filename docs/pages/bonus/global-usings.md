# Global Using

C# 10 introduced **global `using` directives**, a feature that simplifies namespace management by automatically importing commonly used namespaces across an entire project or specific files. With global `using`, developers no longer need to explicitly declare common namespaces in every file, reducing boilerplate code and improving code readability. This is particularly helpful for large projects where the same set of namespaces is used frequently. For example, in a C# project, you can add a global `using` for standard libraries like `System.Linq` or custom namespaces, making them available across all files without needing to declare them repeatedly. This reduces the risk of missing imports and enhances the overall developer experience by streamlining the development process.

In JavaScript/TypeScript, a similar effect can be achieved, but it requires additional tooling or manual effort. One common approach is to create an `index.ts` file that aggregates and exports modules from multiple files, which can then be imported in other parts of the application. While this reduces the number of import statements, it still requires explicit manual construction of the export module and doesn’t provide the same level of automation as C#'s global `using`. Additionally, JavaScript/TypeScript lacks built-in support for globally importing namespaces without extra configuration or custom tooling, meaning developers must rely on bundlers or module systems to handle such imports.

Thus, while JavaScript and TypeScript can achieve similar functionality, C#'s global `using` offers a more seamless and integrated experience, allowing for cleaner and more maintainable code without additional configuration.

## Basics

> 👋🏼 Interested in contributing?
