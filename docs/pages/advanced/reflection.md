# Reflection

C# uses **reflection** capabilities through the `System.Reflection` namespace, allowing developers to inspect metadata about types at runtime such as properties, methods, fields, and their attributes. Reflection in C# is strongly integrated into the language and type system, enabling operations like dynamic method invocation, creating instances of types, and accessing private members, all while maintaining type safety. This allows for powerful scenarios such as dependency injection, serialization, and building flexible, reusable frameworks. Reflection in C# also works seamlessly with generics and provides detailed access to the structure of objects and assemblies, making it a critical tool in advanced scenarios.

In contrast, **TypeScript/JavaScript** has traditionally provided some limited reflection capabilities. JavaScript can inspect objects using features like `typeof`, `instanceof`, or `Object.getOwnPropertyNames()`, but it lacks the comprehensive type introspection found in C#. TypeScript adds some support for type reflection during development through type annotations and the `typeof` operator, but this is mainly for compile-time type checking, not runtime introspection. To achieve more advanced reflection-like functionality, such as schema validation or dynamically interacting with object properties, developers often rely on third-party libraries (like `class-transformer`, `class-validator`, or `reflect-metadata`). Additionally, TypeScript’s type system doesn’t exist at runtime, so true runtime type introspection requires extra effort, including manual schema definitions (e.g. Zod) or metadata decorators, making it less seamless than C#'s built-in reflection tools.

## Basics

> 👋🏼 Interested in contributing?
