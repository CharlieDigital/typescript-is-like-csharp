<script setup>
import CodeSplitter from './components/CodeSplitter.vue'
</script>

# Null Handling

TypeScript `null` and C# `null` are semantically identical.  C# does not have a concept of `undefined`.

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
