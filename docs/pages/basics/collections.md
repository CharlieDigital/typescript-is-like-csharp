# Collections

In JavaScript and TypeScript, the primary collection type is `Array<T>`, which is dynamic, resizable, and can hold elements of any type (though TypeScript allows type constraints like `number[]`). Arrays support powerful methods like `map`, `filter`, and `reduce`. JavaScript also has `Map` and `Set` for key-value storage and unique element collections, respectively. Objects (`{}`) are often used as key-value stores but lack the built-in iteration features of `Map`.

C# provides multiple collection types with strong typing and optimized performance. The `List<T>` class is the closest equivalent to JavaScript’s `Array<T>`, offering dynamic resizing and methods like `Add`, `Remove`, and `ForEach`. For key-value storage, C# uses `Dictionary<TKey, TValue>`, similar to JavaScript’s `Map`, but with strict key typing. Additionally, `HashSet<T>` provides a unique-value collection like JavaScript’s `Set`. Unlike JavaScript arrays, C# also has fixed-size `Array` (`int[] numbers = new int[5];`), which must have a predefined length, making it more memory-efficient.

A key distinction is that a TypeScript collection like `const dogs: Dog[] = []` will happily accept a `{ breed: 'Siamese', purrs: true }` or even `42` at runtime without complaint while C# will throw an exception at runtime if the inserted entity does not have the `Dog` type metadata.

## Arrays

<CodeSplitter>
  <template #left>

```ts
// Explicit type
let pets: string[] = ["Tomi", "Rascal", "Puck"];

// Implicit type
let pets2 = ["Tomi", "Rascal", "Puck"];

// Copy
let pets3 = [...pets2];

// Access
let tomi = pets3[0]; // "Tomi"

// Slice
pets3.slice(0, 2) // ["Tomi", "Rascal"]
```

  </template>
  <template #right>

```csharp
// Explicit type
string[] pets = ["Tomi", "Rascal", "Puck"];

// Implicit type
var pets2 = new[] {"Tomi", "Rascal", "Puck"};

// Copy (Need explicit type here)
string[] pets3 = [.. pets2];

// Access
var tomi = pets3[0]; // "Tomi"

// Slice
pets3[0..2] // ["Tomi", "Rascal"]
```

  </template>
</CodeSplitter>

::: tip
An important note here is that the `Array` type in JavaScript encapsulates multiple semantics including stacks and queues in one.  In C#, these are distinct types provided by the standard library.
:::

::: warning
Unlike JavaScript arrays, C# arrays are **fixed size** at initialization.  To increase the size of the array, a new allocation will be necessary.  C# `List<T>` represents a dynamically sized array-like structure.
:::

## Lists

<CodeSplitter>
  <template #left>

```ts
// Initializer
let friends: string[] = [ "Christi" ];

// Add to end
friends.push("Ram");
friends.push("Minli");

// Access
let ram = friends[1]; // "Ram"
```

  </template>
  <template #right>

```csharp
// Initializer
var friends = new List<string> { "Christi" };

// Add to end
friends.Add("Ram");
friends.Add("Minli");

// Access
var ram = friends[1]; // "Ram"
```

  </template>
</CodeSplitter>

In C#, a `List<T>` is a collection that can grow as new elements are added.

## Stacks

<CodeSplitter>
  <template #left>

```ts
let tasks: string[] = [];
tasks.push("task1");
tasks.push("task2");
let task2 = tasks.pop(); // "task2"

// Peek
var task1 = tasks.pop(); // "task1"
tasks.push(task1);
```

  </template>
  <template #right>

```csharp
var tasks = new Stack<string>();
tasks.Push("task1");
tasks.Push("task2");
var task2 = tasks.Pop(); // "task2"

// Peek
var task1 = tasks.Peek(); // "task1"
```

  </template>
</CodeSplitter>

## Queues

<CodeSplitter>
  <template #left>

```ts
let tasks: string[] = [];
tasks.push("task1");
tasks.push("task2");
let task1 = tasks.shift(); // "task1"

// Peek
let task2 = tasks.shift(); // "task2"
tasks.unshift(task1);
```

  </template>
  <template #right>

```csharp
var tasks = new Queue<string>();
tasks.Enqueue("task1");
tasks.Enqueue("task2");
var task1 = tasks.Dequeue(); // "task1"

// Peek
var task2 = tasks.Peek(); // "task2"
```

  </template>
</CodeSplitter>

## Dictionaries/Maps

TypeScript has two main dictionary/map types: `Record<TKey, TValue>` and `Map<TKey, TValue>` (which preserves order of insertion).  Let's look at both and how they map to C#.

<CodeSplitter>
  <template #left>

```ts
let nameToAge = new Map<string, number>([
  ["Anne", 12],
  ["Bert", 23],
  ["Carl", 43],
]);

nameToAge.set("Didi", 55);

// Enumerate
for (const entry of nameToAge.values()) {
    console.log(entry); // 12, 23, 43, 55
}
```

  </template>
  <template #right>

```csharp
var nameToAge = new OrderedDictionary<string, int> {
  ["Anne"] = 12,
  ["Bert"] = 23,
  ["Carl"] = 43,
};

nameToAge.Add("Didi", 55);

// Enumerate
foreach (var entry in nameToAge.Values) {
  Console.WriteLine(entry); // 12, 23, 43, 55
}
```

  </template>
</CodeSplitter>

Here, the [`OrderedDictionary`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.specialized.ordereddictionary?view=net-9.0) type preserves the order of insertion like TypeScript `Map`.

Alternatively, if the order of insertion doesn't matter:

<CodeSplitter>
  <template #left>

```ts
let nameToAge: Record<string, number> = {
  "Anne": 12,
  "Bert": 23,
  "Carl": 43
}

nameToAge["Didi"] = 55;

// Enumerate
for (const entry of Object.values(nameToAge) {
  console.log(entry); // Unordered
}
```

  </template>
  <template #right>

```csharp
var nameToAge = new Dictionary<string, int> {
  ["Anne"] = 12,
  ["Bert"] = 23,
  ["Carl"] = 43,
};

nameToAge.Add("Didi", 55);

// Enumerate
foreach (var entry in nameToAge.Values) {
  Cosole.WriteLine(entry); // Unordered
}
```

  </template>
</CodeSplitter>

## Sets

<CodeSplitter>
  <template #left>

```ts
let uniqueIds = new Set<number>();
uniqueIds.add(5);
uniqueIds.add(1);
uniqueIds.add(5);

for (const id of uniqueIds.values()) {
  console.log(id); // 5, 1
}
```

  </template>
  <template #right>

```csharp
var uniqueIds = new HashSet<int>();
uniqueIds.Add(5);
uniqueIds.Add(1);
uniqueIds.Add(5);

for (var id in uniqueIds) {
  Console.WriteLine(id); // 5, 1
}
```

  </template>
</CodeSplitter>

## Advanced

This section introduced some of the congruent collection types, but there are several other interesting collection types in the .NET standard libraries that have useful semantics such as:

- [readonly](https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.readonlycollection-1?view=net-9.0) (a readonly wrapper around an underlying list),
- [immutability](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0) (creates a new copy of the list when the list is modified),
- [concurrent read/write](https://learn.microsoft.com/en-us/dotnet/standard/collections/thread-safe/) (used in multi-threaded scenarios for thread-safe access),
- [memory-mapped/inlined collections for speed](https://learn.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay) (managed access to contiguous regions of memory)

In Node, one might typically import a 3rd party library for these types of semantic wrappers around the native collection types (and of course, there's no need for support for concurrent access nor memory inlined collections).  It's nice that these are packaged as part of the .NET standard libraries.
