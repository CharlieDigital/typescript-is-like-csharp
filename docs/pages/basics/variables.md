# Variables

In TypeScript and JavaScript, variables are dynamically typed and can hold values of any type unless explicitly constrained. You declare them using `var`, `let`, or `const`, with `let` and `const` being block-scoped and preferred for modern development. TypeScript introduces static typing through type annotations (e.g., `let x: number = 10;`), but at runtime, JavaScript remains dynamically typed, allowing variables to change types freely.

C#, on the other hand, is a statically typed language where variable types are enforced at compile time *and runtime*. Variables are typically declared with explicit types (`int x = 10;`), ensuring type safety. However, C# also supports type inference using `var` (`var x = 10;`), where the compiler infers the type but does not allow reassignment to a different type. Unlike JavaScript, variables in C# are strongly typed, meaning once a variable is declared with a type, it cannot hold a value of another type without explicit conversion.

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

::: warning Use C# `record` classes for immutability
C#'s `const` keyword does not mean the same thing as in JS. [See the docs](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const) to understand the `const` designator in C#.

To achieve immutability, use C# `record` class types (which we'll visit later in [Classes and Types](./classes.md#record-classes)).
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
