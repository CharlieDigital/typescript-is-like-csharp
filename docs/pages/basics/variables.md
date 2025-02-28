# Variables

## Inferred Types

<CodeSplitter>
  <template #left>

```ts
var x = 1;  // Hoisted
let x = 1;  // Block scope
const x = 1;  // Block scope; immutable
```

  </template>
  <template #right>

```csharp
var x = 1;  // Block scope
const x = 1;  // Compiler "inlined"; NOT the same as JS const
```

  </template>
</CodeSplitter>

::: warning
C#'s `const` keyword does not mean the same thing as in JS. [See the docs](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)
:::

## Explicit Types

<CodeSplitter>
  <template #left>

```ts
// Primitives
let x:number = 1;
let y:string = "";

// Reference types
let map = new Map();
```

  </template>
  <template #right>

```csharp
// Primitives
int x = 1;
string y = "";

// Reference types
let map = new HashMap();
HashMap map = new(); // Means the same thing.
```

  </template>
</CodeSplitter>

## Generic Types

<CodeSplitter>
  <template #left>

```ts
let x: Result<User> = getUser();
```

  </template>
  <template #right>

```csharp
Result<User> x = GetUser();
```

  </template>
</CodeSplitter>

## Collection Initialization

<CodeSplitter>
  <template #left>

```ts
let x = ["Bird", "Cat", "Dog"];
let y = [...x];
```

  </template>
  <template #right>

```csharp
string[] x = ["Bird", "Cat", "Dog"];
string[] y = [..x];
```

  </template>
</CodeSplitter>
