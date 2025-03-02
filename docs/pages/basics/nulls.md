# Null Handling

In JavaScript and TypeScript, `null` and `undefined` are distinct values. `null` is an intentional absence of a value, while `undefined` represents a variable that has been declared but not assigned a value. JavaScript allows both `null` and `undefined` to be used interchangeably in some cases (e.g., loose equality comparisons), but TypeScript can enforce stricter handling with the `strictNullChecks` option, preventing unintended `null` or `undefined` assignments unless explicitly allowed.

C# does not have `undefined`; every variable must have a defined value. Value types (e.g., `int`, `bool`) cannot be `null` unless explicitly made nullable using `?` (e.g., `int? x = null;`). Reference types (e.g., `string`, `object`) can be `null` by default. To improve null safety, C# includes nullable reference types (`string? name = null;`), allowing developers to indicate which variables can be `null` and leveraging compiler warnings to prevent unintended `null` dereferences.

## Nullability

<CodeSplitter>
  <template #left>

```ts
let x: string | null;

function findUser(name: string, email?: string ) {
  if (email?.trim()) {
    // Handle case when email is null or zero length
  }
}

let handle = email?.split("@")[0];

// Null forgiving operator
let handle = email!.split("@")[0];
```

  </template>
  <template #right>

```csharp
string? x;

User[] FindUser(string name, string? email) {
  if (string.IsNullOrWhiteSpace(email)) {
    // Handle case when email is null or zero length
  }
}

var handle = email?.Split("@")[0];

// Null forgiving operator
var handle = email!.Split("@")[0];
```

  </template>
</CodeSplitter>

## Null Coalescing

<CodeSplitter>
  <template #left>

```ts
let handle = email?.split("@")[0] ?? userId;

handle ??= "unknown"
// 👆👇These are equivalent
if (handle == null) {
  handle == "unknown"
}
```

  </template>
  <template #right>

```csharp
var handle = email?.Split("@")[0] ?? userId;

handle ??= "unknown"
// 👆👇These are equivalent
if (handle == null) {
  handle == "unknown"
}
```

  </template>
</CodeSplitter>

In C#, we can also use *[pattern matching](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns)* with `null` values like this:

```csharp
class User {
  public int Id { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
}

if (user is { FirstName: null, Id: > 10000 }) {
  // FirstName is null and ID is greater than 10000
}
```

Tim Deschryver has [the best writeup on pattern matching](https://timdeschryver.dev/blog/pattern-matching-examples-in-csharp#tuple-patterns)

## Nullability with Generics

<CodeSplitter>
  <template #left>

```ts
type List<T> = { }

// "Elements are string or null"
let list: List<string | null>;

let arr: Array<string | null>[];
```

  </template>
  <template #right>

```csharp
// List<T> is standard library collection type

// "Elements are string or null"
List<string?> list;

string?[] arr;
```

  </template>
</CodeSplitter>
