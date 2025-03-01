# Iterators and Enumerables

TypeScript [iterators](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Iterator) map to C# [`Enumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-9.0) / [`IEnumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-9.0).  These abstractions provide forward, read-only semantics.

Most generally, they represent a forward-only "virtual" iteration over a set.

## Working with Iterators and Enumerables

<CodeSplitter>
  <template #left>

```ts
let nameToAge = new Map<string, number>([
  ["Anne", 12],
  ["Bert", 23],
  ["Carl", 43],
]);

// Enumerate
for (const entry of nameToAge.values()) {
    console.log(entry) // 12, 23, 43
}

// Convert to array
let ages = Array.from(nameToAge.values());
```

  </template>
  <template #right>

```csharp
var nameToAge = new OrderedDictionary<string, int> {
  ["Anne"] = 12,
  ["Bert"] = 23,
  ["Carl"] = 43,
};

// Enumerate
foreach (var entry of nameToAge.Values) {
  console.log(entry) // 12, 23, 43
}

// Convert to List<T> (T is inferred automatically)
var ages = nameToAge.Values.ToList();
```

  </template>
</CodeSplitter>

## Generators
