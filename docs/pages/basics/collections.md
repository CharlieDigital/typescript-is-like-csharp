# Collections

C# differentiates several types of collection semantics into distinct classes (which have an underlying `Array` (or possibly `Span`) implementation).

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
Unlike JavaScript arrays, C# arrays a **fixed size** at initialization.  To increase the size of the array, a new allocation will be necessary.  C# `List<T>` represents a dynamically sized array-like structure.
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

## Sets

## Advanced

This section introduced some of the congruent collection types, but there are several other interesting collection types in the .NET standard libraries that have useful semantics such as:

- readonly,
- immutability,
- concurrent read/write,
- memory-mapped/inlined collections for speed

Read more to find out how these types work and why they are useful.  These can all be obtained through NPM packages, but it's nice that these are packaged as part of the .NET standard libraries.
