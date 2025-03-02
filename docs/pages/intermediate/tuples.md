# Tuples

Tuples in both C# and TypeScript/JavaScript are used to group multiple values of potentially different types into a single object, but there are key differences in syntax and functionality. In TypeScript/JavaScript, a tuple is simply an array with fixed types for each element, and the types are enforced by TypeScript during development (`let myTuple: [string, number] = ["hello", 42];`). JavaScript itself does not have a distinct tuple type, so tuples are essentially arrays with a defined number of elements and types. TypeScript enforces the type constraints at compile time, making tuples more predictable.

In C#, tuples are more robust, and with **C# 7.0 and later**, C# introduces **named tuples**, allowing for greater clarity and better code readability. A C# tuple is defined with parentheses and can hold different types of values, with optional **names** for each item (`var myTuple = (Name: "John", Age: 30);`). Named tuples provide clear context to the data, improving the readability of the code. Additionally, C# tuples support destructuring, so you can assign values to variables by name (`var (name, age) = myTuple;`). While TypeScript allows destructuring of tuples, it doesn't have native support for named elements, making C# tuples a more feature-rich, readable option.

## Basics

> 👋🏼 Interested in contributing?
