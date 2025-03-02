# Iteration

C# and TypeScript/JavaScript share many iteration constructs, including `for`, `while`, and `do-while` loops, but C# provides additional iteration mechanisms that enhance readability and safety. The classic `for` loop works the same in both languages, iterating with an initializer, condition, and increment (`for (let i = 0; i < 10; i++)` in JavaScript vs. `for (int i = 0; i < 10; i++)` in C#). Likewise, `while` and `do-while` loops function similarly. However, in C#, conditions must explicitly evaluate to `bool`, avoiding JavaScript's loose type coercion issues (e.g., `while ("0")` is valid in JavaScript but invalid in C#).

C# also introduces the **foreach** loop, which simplifies iteration over collections (`foreach (var item in collection)`). While JavaScript/TypeScript offers `for...of` for iterating over iterable objects, `forEach` for arrays, and `for...in` for object properties, C#’s `foreach` is more type-safe and works seamlessly with **IEnumerable&lt;T&gt;** collections, preventing off-by-one errors and ensuring safer iteration. Additionally, C#’s [**LINQ** (Language Integrated Query)](../intermediate/linq.md) enables functional-style iteration using methods like `.Select()` and `.Where()`, offering a more declarative alternative to traditional loops—something JavaScript often achieve with array methods like `.map()` and `.filter()`.

## `for` and `foreach`

<CodeSplitter>
  <template #left>

```ts
for (const i = 0; i < 10; i++) {

}

for (const entry of entries) {

}
```

  </template>
  <template #right>

```csharp
for (var i = 0i; i < 10; i++) {

}

foreach (var entry in entries) {

}
```

  </template>
</CodeSplitter>

## `while` and `do-while`


<CodeSplitter>
  <template #left>

```ts
let count = 0

while (count < 10) {
  count++;
}

do {
  count++;
} while (count < 20);
```

  </template>
  <template #right>

```csharp
var count = 0

while (count < 10) {
  count++;
}

do {
  count++;
} while (count < 20);
```

  </template>
</CodeSplitter>

## Iterators and Enumerables

These are broken out into their own section.
