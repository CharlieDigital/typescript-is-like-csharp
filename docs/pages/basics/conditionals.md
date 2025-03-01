# Conditionals

## If-Else

<CodeSplitter>
  <template #left>

```ts
if (x == 5) { /* ... */ }
if (x < 5) { /* ... */ }
if (x >= 5) { /* ... */ }
if (x % 5 == 0) { /* ... */ }

if (y == 10) {

} else {

}
```

  </template>
  <template #right>

```csharp
if (x == 5) { /* ... */ }
if (x < 5) { /* ... */ }
if (x >= 5) { /* ... */ }
if (x % 5 == 0) { /* ... */ }

if (y == 10) {

} else {

}
```

  </template>
</CodeSplitter>

::: tip
See below for how we can combine *pattern matching* with `if` conditions
:::

## Switch-Case

<CodeSplitter>
  <template #left>

```ts
switch (x) {
  case 0:
    console.log("You win a dollar!");
    break;
  case 5:
    console.log("You win a car!");
    break;
  default:
    console.log("Play again!");
    break;
}
```

  </template>
  <template #right>

```csharp
switch (x) {
  case 0:
    Console.WriteLine("You win a dollar!");
    break;
  case 5:
    Console.WriteLine("You win a car!");
    break;
  default:
    Console.WriteLine("Play again!");
    break;
}
```

  </template>
</CodeSplitter>

::: tip
See below for how we can combine *pattern matching* with `switch-case`
:::

## Ternary

<CodeSplitter>
  <template #left>

```ts
let y = x > 5 ? "yes" : "no";
```

  </template>
  <template #right>

```csharp
var y = x > 5 ? "yes" : "no";
```

  </template>
</CodeSplitter>

## C# Pattern Matching

C# has two additional ways of representing conditionals.  The first is pattern matching.

### Switch-Case

```csharp
// Single expression
switch (x) {
  case <= 100 when x > 98: // 👈 Note the pattern
    Console.WriteLine("You got an A+!");
    break;
  case <= 98 when x >= 90: // 👈 Note the pattern
    Console.WriteLine("You got an A!");
    break;
  default:
    Console.WriteLine("You passed!");
    break;
}

// Tuple expression
switch ( (coursework, midterm, finals) ) {
  case (> 80, > 90, > 95): // 👈 Note the pattern
  case (> 90, > 90, > 90): // 👈 Note the pattern
    Console.WriteLine("You got an A!");
    break;
  case (> 80, > 80, > 80): // 👈 Note the pattern
    Console.WriteLine("You got a B!");
    break;
  default:
    Console.WriteLine("You passed!");
    break;
}
```

### If-Else

```csharp
var account = ("Diamond Member", 100_000);

if (account is ("Diamond Member", >= 100_000)) {
  Console.WriteLine("You are a VIP!");
} else if (account is ("Gold Member", >= 50_000)) {
  Console.WriteLine("You are a Gold Member!");
} else if (account is ("Silver Member", >= 10_000)) {
  Console.WriteLine("You are a Silver Member!");
} else {
  Console.WriteLine("You are a regular member.");
}

// You are a VIP!
```

Here again, we use it with a tuple type, but we can also use it with normal and [*anonymouse* types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types):

```csharp
var account = new {
  Name = "Gigi",
  Balance = 100_000
};

if (account is { Balance: >= 100_000 }) {
  Console.WriteLine($"{account.Name} is a VIP!");
} else if (account is { Balance: >= 50_000 }) {
  Console.WriteLine($"{account.Name} is a Gold Member!");
} else if (account is { Balance: >= 10_000 }) {
  Console.WriteLine($"{account.Name} is a Silver Member!");
} else {
  Console.WriteLine($"{account.Name} is a regular member.");
}

// "Gigi is a VIP"
```

## C# Switch Expressions

The second additional way C# can express conditionals is switch expressions:

```csharp
var courseGrade = (87, 91, 98) switch {
  ( > 90, > 90, > 95) => "A+",
  ( > 80, > 90, > 90) => "A",
  ( > 80, > 80, > 80) => "B",
  _ => "C"
};

Console.WriteLine(courseGrade); // "A"
```

Switch expressions have a lot of utility and can condense otherwise complex trees of conditional logic!  They are broken out into more detail in its own doc.  If you want to explore more, checkout [Tim Deschryver's excellent writeup on how powerful patterns and switch expressions are in C#](https://timdeschryver.dev/blog/pattern-matching-examples-in-csharp#tuple-patterns)
