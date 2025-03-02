# LINQ

LINQ (Language Integrated Query) in C# enables functional programming by providing a set of methods like `Where()`, `Select()`, and `OrderBy()` to query and transform collections. LINQ operates on **`IEnumerable<T>`**, supporting **deferred execution**, meaning queries aren’t executed until they are iterated over. This approach is more memory-efficient because it avoids allocating intermediate collections. For example, chaining `Where()` and `Select()` in LINQ does not create temporary arrays, making it suitable for working with large datasets.

In JavaScript and TypeScript, similar operations are performed using methods like `filter()`, `map()`, and `reduce()`, which achieve the same transformations as LINQ’s `Where()` and `Select()`. However, JavaScript methods typically execute eagerly, creating new arrays for each operation. This can lead to higher memory usage and slower performance for large collections. C#’s deferred execution and use of `IEnumerable<T>` allow LINQ to be more memory-efficient and performant, especially when chaining multiple operations.

## Basics

> 👋🏼 Interested in contributing?
