# Records

In C#, **records** are a special type of reference type introduced in C# 9.0, designed for representing data that is **immutable** by default. A record provides built-in value-based equality, meaning two records with the same data are considered equal, unlike classes, which use reference equality. Records automatically generate methods like `Equals()`, `GetHashCode()`, and `ToString()`, reducing boilerplate code. The syntax for defining a record is concise, and it often serves to model data structures like DTOs (Data Transfer Objects) or immutable domain entities (`public record Person(string Name, int Age);`).

Immutability in records is particularly useful in scenarios where **thread safety** and **predictability** are important. For example, records are ideal for representing values that should not change after creation, such as configuration settings, data returned from APIs, or event data in event-driven systems. Since records cannot be modified after creation, they avoid common pitfalls associated with mutable objects, like unintended side effects or bugs from shared state in multi-threaded environments. Immutability also makes code easier to reason about, as the data is guaranteed not to change unexpectedly, which is useful in functional programming styles, concurrency, and distributed systems where data consistency is crucial.

## Basics

> 👋🏼 Interested in contributing?
