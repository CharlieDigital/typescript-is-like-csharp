# Generators and `yield`

Both C# and JavaScript/TypeScript use **generator functions** and the `yield` keyword to create iterators that allow functions to return values lazily without allocating memory for intermediate results. In JavaScript/TypeScript, a generator is defined using the `function*` syntax, and the `yield` keyword produces values one at a time. The function’s execution is paused and resumed using the `.next()` method of the iterator. This makes it useful for scenarios like lazy evaluation or asynchronous programming, where values are needed on demand, without the overhead of creating entire collections.

In C#, generators are implemented using `yield return` within methods that return an `IEnumerable<T>` or `IEnumerator<T>`. Like in JavaScript, C# generators allow you to produce values lazily, but they work within the `IEnumerable<T>` interface, where values are retrieved via `MoveNext()` and `Current`. A key benefit of both C# and JavaScript generators is that they are **allocationless**, meaning they do not require storing intermediate collections in memory, which makes them highly efficient when dealing with large datasets or infinite sequences.

## Basics

> 👋🏼 Interested in contributing?
