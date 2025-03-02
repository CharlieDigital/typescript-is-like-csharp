# Strings

In JavaScript and TypeScript, strings are represented by the `string` type and are immutable sequences of characters. Strings are defined using single (`'`), double (`"`), or backtick (\`\`) quotes, with backticks enabling template literals for interpolation (e.g., `` `Hello, ${name}!` ``). There is no distinct `char` type; single characters are simply strings of length one (`const letter: string = 'A';`).

C# also has an immutable `string` type, which is a sequence of UTF-16 characters. Strings are defined using double quotes (`string message = "Hello";`) and support interpolation with `$` (`$"Hello, {name}!"`). Unlike JavaScript, C# has a distinct `char` type for single characters (`char letter = 'A';`), which uses single quotes and represents a single UTF-16 character rather than a string. This distinction is important because `char` is a value type, while `string` is a reference type, impacting performance and memory usage.

## Basics

<CodeSplitter>
  <template #left>

```ts
let name = "Steve";
let name: string = "Steve";
```

  </template>
  <template #right>

```csharp
var name = "Steve";
string name = "Steve";
```

  </template>
</CodeSplitter>

::: tip
In C#, the `''` is a designator for a `char` or character type (as opposed to `""` for `string` types.).
-  `'c'` is the `char` type representation of `c`
-  `"c"` is the `string` type representation of `c`
:::

## Multi Line

<CodeSplitter>
  <template #left>

```ts
let html = `
  <div>
    <p>Hello!</p>
  </div>
`;
```

  </template>
  <template #right>

```csharp
var html = """
  <div>
    <p>Hello!</p>
  </div>
  """; // Note the left alignment.
```

  </template>
</CodeSplitter>

::: info
See more about C#'s [multi-line literals](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string).
:::

## Interpolation

<CodeSplitter>
  <template #left>

```ts
let name = `${first} ${last}`;

let html = `
  <div>
    <p>${greeting}</p>
  </div>
`;
```

  </template>
  <template #right>

```csharp
var name = $"{first} {last}";

var html = $"""
  <div>
    <p>{greeting}</p>
  </div>
  """;
```

  </template>
</CodeSplitter>

## Substrings

<CodeSplitter>
  <template #left>

```ts
let name = "Juan";
let a = name.slice(0, 1) // J
let b = name.slice(0, 1) // J
let c = name.slice(-2, -1) // a
let d = name.slice(-2) // an
let e = name.slice(1,-2) // u
```

  </template>
  <template #right>

```csharp
var name = "Juan";
var a = name[0..1]; // J
var b = name[..1]; // J
var c = name[^2]; // a
var d = name[^2..]; // an
var e = name[1..^2]; // u
```

  </template>
</CodeSplitter>

::: info
Learn more about C# [ranges and indices](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/ranges-indexes)
:::
