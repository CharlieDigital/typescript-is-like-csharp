# Comments

C# and TypeScript/JavaScript use the same basic comment syntax: `//` for single-line comments and `/* */` for multi-line comments. However, C# introduces **XML documentation comments**, written with `///`, which integrate with tools like IntelliSense and generate API documentation (`/// <summary>Describes a method</summary>`). TypeScript supports JSDoc-style comments (`/** ... */`), which provide similar documentation capabilities but rely on external tooling rather than built-in language support.

Unlike JavaScript, where comments are purely for human readability, C#'s XML comments can enforce structured documentation and provide compile-time feedback. While both languages encourage clear commenting practices, C#’s built-in documentation system makes it easier to maintain well-documented code, especially for large projects and libraries.

## Inline

<CodeSplitter>
  <template #left>

```ts
// Single line
if (x == y) { }

if (x == y) { } // Single line
```

  </template>
  <template #right>

```csharp
// Single line
if (x == y) { }

if (x == y) { } // Single line
```

  </template>
</CodeSplitter>

## Block

<CodeSplitter>
  <template #left>

```ts
/*
Block comment
*/
if (a == b) { }

if (a == b) { /* Inline block comment */ }
```

  </template>
  <template #right>

```csharp
/*
Block comment
*/
if (a == b) { }

if (a == b) { /* Inline block comment */ }
```

  </template>
</CodeSplitter>

## Class and Function

<CodeSplitter>
  <template #left>

```ts
/**
 * Adds two numbers
 * @param a First number
 * @param b Second number
 * @returns The sum of a and b
 */
function sum(a: number, b: number) : number { }
```

  </template>
  <template #right>

```csharp
/// <summary>
/// Adds two numbers
/// </summary>
/// <param name="a">First number</param>
/// <param name="b">Second number</param>
/// <returns>The sum of a and b</returns>
int Sum(int a, int b) { }
```

  </template>
</CodeSplitter>

::: warning
Because C# comments are XML, they cannot contain `<`, `>`, `&`, and a few other XML entities.  Use `&lt;`, `&gt;`, or `&amp;` if you must.  But generally, consider alternative ways of representation.
:::

::: tip
It is very tedious to type out the XML!  So get an extension for your IDE that does it automatically when you type `///`.
:::
