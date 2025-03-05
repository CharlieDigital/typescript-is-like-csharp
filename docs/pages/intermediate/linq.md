# LINQ

LINQ (Language Integrated Query) is possibly one of the "killer apps" of C# as it is integrated into a variety of contexts including just working with collections as well as writing database queries via the Entity Framework ORM.  It offers a *superset* of the features that JavaScript arrays offer when it comes to functional operations over a collection.

LINQ in C# enables functional programming by providing a set of methods like `Where()`, `Select()`, and `OrderBy()` to query and transform collections. LINQ operates on **`IEnumerable<T>`**, supporting [**deferred streaming execution** (where applicable)](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries#classification-table), meaning queries aren’t executed until they are iterated over. This approach is more memory-efficient because it avoids allocating intermediate collections. For example, chaining `Where()` and `Select()` in LINQ does not create temporary arrays, making it suitable for working with large datasets.

In JavaScript and TypeScript, similar operations are performed using methods like `filter()`, `map()`, and `reduce()`, which achieve the same transformations as LINQ’s `Where()` and `Select()`. However, JavaScript methods typically execute eagerly, creating new arrays for each operation. This can lead to higher memory usage and slower performance for large collections. C#’s deferred execution and use of `IEnumerable<T>` allow LINQ to be more memory-efficient and performant, especially when chaining multiple operations.

## Setup

Our examples below will assume the following starting model:

<CodeSplitter>
  <template #left>

```ts
type Position = 'frontend' | 'backend' | 'database' | 'infra'

type Candidate = {
  name: string
  position: Position,
  yoe: number
  tech: string[]
}

let candidates = [
  { name: "Ada", position: 'backend', yoe: 5, tech: ["C#", "Node.js", "Go"] },
  { name: "Alan", position: 'frontend', yoe: 3, tech: ["React", "Vue", "C#", "MongoDB"] },
  { name: "Charles", position: 'database', yoe: 7, tech: ["Postgres", "MongoDB"] }
];
```

  </template>
  <template #right>

```csharp
enum Position { Frontend, Backend, Database, Infra }

record Candidate(
  string Name,
  Position Position,
  int YoE,
  string[] Tech
);

var candidates = new List<Candidate> {
  new("Ada", Position.Backend, 5, ["C#", "Node.js", "Go"]),
  new("Alan", Position.Frontend, 3, ["React", "Vue", "C#", "MongoDB"]),
  new("Charles", Position.Database, 7, ["Postgres", "MongoDB"])
};
```

  </template>
</CodeSplitter>

## Filtering and Projecting

<CodeSplitter>
  <template #left>

```ts
// Filter
let backend = candidates.filter(
  c => c.position === 'backend'
); // { Ada }

// Project
let names = candidates.map(
  c => c.name
); // ["Ada", "Alan", "Charles"]

// Combine
let backendNames = candidates.filter(
  c => c.position === 'backend'
).map(
  c => c.name
); // ["Ada"]
```

  </template>
  <template #right>

```csharp
// Filter
var backend = candidates.Where(
  c => c.Position == Position.Backend
); // { Ada }

// Project
var names = candidates.Select(
  c => c.Name
); // ["Ada", "Alan", "Charles"]

// Combine
var backendNames = candidates.Where(
  c => c.position === 'backend'
).Select(
  c => c.name
); // ["Ada"]
```

  </template>
</CodeSplitter>

Here, we filter by min/max:

<CodeSplitter>
  <template #left>

```ts
let minExp = candidates.reduce(
  (prev, curr) => prev.yoe < curr.yoe ? prev : curr
);
console.log(minExp.name); // "Alan"

let maxExp = candidates.reduce(
  (prev, curr) => prev.yoe > curr.yoe ? prev : curr
);
console.log(maxExp.name); // "Charles"
```

  </template>
  <template #right>

```csharp
var minExp = candidates.MinBy(c => c.YoE);
Console.WriteLine(minExp.Name); // "Alan"

var maxExp = candidates.MaxBy(c => c.YoE);
Console.WriteLine(maxExp.Name); // "Charles"
```

  </template>
</CodeSplitter>

In JS/TS, we can add methods to the `Array` type via `Prototype` or use a 3rd party package, but these are already built into .NET's standard libraries.

## Reducing and Aggregating

<CodeSplitter>
  <template #left>

```ts
// Accumulate a map of the tech to the candidates
let techToCandidates = candidates.reduce(
  (map, c) => {
    for (let t of c.tech) {
      if (!map.has(t)) {
        map.set(t, [])
      }

      map.get(t)!.push(c.name)
    }

    return map
  },
  new Map<string, string[]>()
);
```

  </template>
  <template #right>

```csharp
// Accumulate a map of the tech to the candidates
var techToCandidates = candidates.Aggregate(
  new Dictionary<string, List<string>>(),
  (map, c) => {
    foreach (var t in c.Tech) {
      if (!map.ContainsKey(t)) {
        map[t] = new();
      }

      map[t].Add(c.Name);
    }

    return map;
  }
);
```

  </template>
</CodeSplitter>

Both return a similar structure:

```json
{
  "C#": ["Ada", "Alan"],
  "Node.js": ["Ada"],
  "Go": ["Ada"],
  "React": ["Alan"],
  "Vue": ["Alan"],
  "MongoDB": ["Alan", "Charles"],
  "Postgres": ["Charles"]
}
```

Another example here where we perform a simple sum of all years of experience:

<CodeSplitter>
  <template #left>

```ts
let totalYoe = candidates.reduce(
  (yoe, c) => yoe + c.yoe, 0
)
console.log(totalYoe); // 15

let totalYoe2 = candidates
  .filter(c => c.startsWith("A"))
  .reduce((yoe, c) => yoe + c.yoe, 0)
console.log(totalYoe); // 8
```

  </template>
  <template #right>

```csharp
var totalYoe = candidates.Sum(c => c.YoE);
Console.WriteLine(totalYoe); // 15

var totalYoe2 = candidates
  .Where(c => c.Name.StartsWith("A"))
  .Sum(c => c.YoE);
Console.WriteLine(totalYoe2);  // 8
```

  </template>
</CodeSplitter>

## Read More

C#'s `System.Linq` library offers a superset of functionality to JavaScript array operators.  [Check out the docs for more examples](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-9.0) including operators like `.Min()`/`.Max()`, `.Skip()`, `.Take()`, `.TakeWhile()`, and more!

We'll also look at how LINQ gets used later in .NET's extremely powerful first party ORM [Entity Framework Core](ef-core.md)
