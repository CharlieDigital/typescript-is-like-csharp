# Tuples

Tuples in both C# and TypeScript/JavaScript are used to group multiple values of potentially different types into a single object, but there are key differences in syntax and functionality. In TypeScript/JavaScript, a tuple is simply an array with fixed types for each element, and the types are enforced by TypeScript during development (`let myTuple: [string, number] = ["hello", 42];`). JavaScript itself does not have a distinct tuple type, so tuples are essentially arrays with a defined number of elements and types. TypeScript enforces the type constraints at compile time, making tuples more predictable.

In C#, tuples are more robust, and with **C# 7.0 and later**, C# introduces **named tuples**, allowing for greater clarity and better code readability. A C# tuple is defined with parentheses and can hold different types of values, with optional **names** for each item (`var myTuple = (Name: "John", Age: 30);`). Named tuples provide clear context to the data, improving the readability of the code. Additionally, C# tuples support destructuring, so you can assign values to variables by name (`var (name, age) = myTuple;`). While TypeScript allows destructuring of tuples, it doesn't have native support for named elements, making C# tuples a more feature-rich, readable option.

## Basics

As in JS/TS, tuple types in C# provide a convenient way to return sets of values without creating a `class` or `record`.  Unlike anonymous types, we can pass this structure back out of a function (whereas anonymous types become `object`).

<CodeSplitter>
  <template #left>

```ts
type Position = "backend" | "frontend" | "database" | "infra";

// Tuple as return type 👇
function getCandidates() : Array<[string, Position]> {
  return [
    ["Ada", "backend"],
    ["Alan", "frontend"],
    ["Charles", "infra"]
  ]
}

// Destructure 👇
for (let [name, position] of getCandidates()) {
  console.log(`${name}: ${position}`)
}
```

  </template>
  <template #right>

```csharp
enum Position { Frontend, Backend, Database, Infra }

// 👇 Tuple as return type
(string, Position)[] GetCandidates() => new [] {
  ("Ada", Position.Backend), // 👈 Tuple
  ("Alan", Position.Frontend),
  ("Charles", Position.Infra),
};

// Destructure 👇
foreach (var (name, position) in GetCandidates()) {
  Console.WriteLine($"{name}: {position}");
}
```

  </template>
</CodeSplitter>

## Named Tuples

C# allows naming of the tuple indices to make it a bit safer to access the fields correctly.

<CodeSplitter>
  <template #left>

```ts
let ada = ["Ada", "backend"];
let alan = ["Alan", "frontend"];
let charles = ["Charles", "infra"];

console.log(ada[0]); // "Ada"

let [candidateName, position] = alan;
console.log(candidateName); // "Alan"
```

  </template>
  <template #right>

```csharp
var ada = (Name: "Ada", Position: Position.Backend);
var alan = (Name: "Alan", Position: Position.Frontend);
var charles = (Name: "Charles", Position: Position.Infra);

Console.WriteLine(ada.Name); // "Ada"

var (candidateName, position) = alan;
Console.WriteLine(candidateName); // "Alan"
```

  </template>
</CodeSplitter>

Note that we can still destructure the tuple just like before and rename the indices.

## Aliased Tuple Types

[C# 12 introduced the option to alias any type](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12#alias-any-type) which of course includes tuple types!  So we can apply a name to a tuple just like in TypeScript.

<CodeSplitter>
  <template #left>

```ts
type Position = "backend" | "frontend" | "database" | "infra";

type Candidate = [
  number,
  Position
] // 👆 Tuple

let candidates: Record<string, Candidate> = {
  "Ada": [3, 'backend'], // 👈 Tuple
  "Alan": [4, 'frontend'],
  "Charles": [5, 'infra'],
}

function printCandidates(currentCandidates: Record<string, Candidate>) {
  // More tuples 👇
  for (const [key, value] of Object.entries(currentCandidates)) {
    console.log(`${key} has ${value[0]} years of experience and works on ${value[1]}`)
  }
}

printCandidates(candidates);

// Ada has 3 years of experience and works on backend
// Alan has 4 years of experience and works on frontend
// Charles has 5 years of experience and works on infra
```

  </template>
  <template #right>

```csharp
using Candidate = (
  int YoE,
  Position Position
); // 👆 Alias our tuple

enum Position { Frontend, Backend, Database, Infra }

var candidates = new Dictionary<string, Candidate> {
  ["Ada"] = (3, Position.Backend), // 👈 Tuple value
  ["Alan"] = (4, Position.Frontend),
  ["Charles"] = (5, Position.Infra),
};

void PrintCandidates(Dictionary<string, Candidate> currentCandidates) {
  // More tuples 👇
  foreach (var (key, value) in currentCandidates) {
    Console.WriteLine($"{key} has {value.YoE} years of experience and works on {value.Position}");
  }
}

PrintCandidates(candidates);

// Ada has 3 years of experience and works on Backend
// Alan has 4 years of experience and works on Frontend
// Charles has 5 years of experience and works on Infra
```

  </template>
</CodeSplitter>

::: info
Aliased tuples are only valid within a file; outside of the file, the tuple structure remains, but the named type is gone.
:::

Because of the named indices with C# tuples, the overall DX is better and less prone to error versus pure index based access.

## Tuples, Tuples, and More Tuples!

In C#, we can use tuples to mimic how object maps work for in JS/TS. Effectively, tuples let C# deal with "structural types" instead of named types.  But keep in mind that *all of this remains type safe at runtime* in C#.

<CodeSplitter>
  <template #left>

```ts
// This example uses an object to retain field access
type Platform = "Mastodon" | "Bluesky" | "Threads";

type Profile = {
  name: string,
  socials: {
    handle: string,
    platform: Platform
  }[]
}

function getProfiles() : Profile[] {
  return [{
    name: "Charles",
    socials: [
      { handle: "@chrlschn", platform: "Mastodon" },
      { handle: "@chrlschn", platform: "Bluesky" }
    ]
  },
  {
    name: "Sandra",
    socials: [
      { handle: "@sndrchn", platform: "Threads" }
    ]
  }]
}

let profiles = getProfiles();
console.log(profiles[0].name); // "Charles"
console.log(profiles[0].socials[0].handle); // "@chrlschn"
console.log(profiles[1].name); // "Sandra"
console.log(profiles[1].socials[0].handle); // "@sndrchn"
```

  </template>
  <template #right>

```csharp
// An aliased tuple with another tuple as a property `Socials`
using Profile = (
  string Name,
  (
    string Handle,
    Platform Platform
  )[] Socials // 👈 Array of tuples in another tuple
);

enum Platform { Mastodon, Bluesky, Threads }

Profile[] GetProfiles() => new[] {
  ("Charles", new[] {
    ("@chrlschn", Platform.Mastodon),
    ("@chrlschn", Platform.Bluesky),
  }),
  ("Sandra", new[] {
    ("@sndrchn", Platform.Threads)
  }),
};

var profiles = GetProfiles();
Console.WriteLine(profiles[0].Name); // "Charles"
Console.WriteLine(profiles[0].Socials[0].Handle); // "@chrlschn"
Console.WriteLine(profiles[1].Name); // "Sandra"
Console.WriteLine(profiles[1].Socials[0].Handle); // "@sndrchn"
```

  </template>
</CodeSplitter>

::: info Limitations
There are some limits in the C# case as the `using` has to appear at the top of the file.  Additionally, the aliased tuple cannot refer to another aliased tuple.
:::
