# Iteration

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
