# Functions

Functions are **first-class citizens** in both C# and TypeScript/JavaScript, meaning they can be assigned to variables, passed as arguments, and returned from other functions. In JavaScript and TypeScript, functions are objects, allowing flexible definitions with `function` declarations, `function` expressions, and arrow functions (`const add = (a, b) => a + b`). Similarly, C# supports **delegates**, function pointers, and **lambda expressions** (`Func<int, int, int> add = (a, b) => a + b;`), which behave like JavaScript arrow functions but require explicit delegate types.

Both languages support **closures**, where inner functions capture variables from their surrounding scope. In JavaScript, closures are commonly used for encapsulation and callbacks, while in C#, lambda expressions and local functions (`void LocalFunction() { }`) capture outer variables and are often used in LINQ, event handling, and asynchronous programming. However, C# provides additional type safety, requiring explicit delegate types (`Func<T>`, `Action<T>`) for function references, whereas JavaScript functions remain dynamically typed. Despite these differences, both languages treat functions as flexible, reusable constructs that enable functional programming patterns.

## Basics

<CodeSplitter>
  <template #left>

```ts
function fn(
  name: string, // Required parameter
  affiliation: string = "unaffiliated", // Default parameter,
  notify: () => void, // Function parameter
  nickName?: string // Optional parameter
) {
  let x = "1";
  let y = "2";

  // Local function
  let fx = () => {
    console.log(`x = ${x}`);
  }

  // Local function
  function fy() {
    console.log(`y = ${y}`);
  }

  fx(); // "x = 1"
  fy(); // "y = 2"
}
```

  </template>
  <template #right>

```csharp
void fn(
  string name, // Required parameter
  string affiliation = "unaffiliated", // Default parameter
  Action notify, // Function parameter
  string? nickName // Optional parameter
) {
  var x = "1";
  var y = "2";

  // Local function
  var fx = () => {
    Console.WriteLine($"x = {x}");
  }

  // Local function
  void fy() {
    Console.WriteLine($"y = {y}");
  }

  fx(); // "x = 1"
  fy(); // "y = 2"
}
```

  </template>
</CodeSplitter>

::: info
C#'s `Action` and `Func` types are covered below.
:::

## Lambda Expressions

<CodeSplitter>
  <template #left>

```ts
// Lambda expression
let fn = (msg: string) => console.log(msg);

fn("Hello, World!");

let contacts = ["Allie", "Stella", "Carson"];
contacts.forEach(fn)
```

  </template>
  <template #right>

```csharp
// Lambda expression
var fn = (string msg) => Console.WriteLine(msg);

fn("Hello, World!");

// 👇 Here, we use a `List` so we import these
using System.Collections.Generic;
using System.Linq;

var contacts = new List<string> { "Allie", "Stella", "Carson" };
contacts.ForEach(fn);
```

  </template>
</CodeSplitter>

::: tip
Here, we see an intro to .NET's powerful LINQ (Language Integrated Query) features.  We'll dive into this more later.
:::

## Default Parameter Values

<CodeSplitter>
  <template #left>

```ts
function fn(name: string = "(no name)") {
  console.log(`Hello, ${name}`);
}

fn(); // "Hello, (no name)""
fn("Carl"); // "Hello, Carl"
```

  </template>
  <template #right>

```csharp
void fn(string name = "(no name)") {
  Console.WriteLine($"Hello, {name}");
}

fn(); // "Hello, (no name)""
fn("Carl"); // "Hello, Carl"
```

  </template>
</CodeSplitter>

## Passing Named Parameters

TypeScript does not natively have named parameters so we need to destructure an object to achieve the same result.

<CodeSplitter>
  <template #left>

```ts
function fn({
  firstName, // 👈 Use destructuring
  lastName
} : {
  firstName: string,
  lastName: string
}) {
  console.log(`Hello, ${firstName} ${lastName}`);
}

// Pass an object to be destructured
fn({lastName: "Lee", firstName: "Amy"});

// Or with a type:
type Contact = {
  firstName: string,
  lastName: string
}

function fn({
  firstName, // 👈 Use destructuring
  lastName
} : Contact) {
  console.log(`Hello, ${firstName} ${lastName}`);
}

// Pass an object to be destructured
fn({lastName: "Lee", firstName: "Amy"});
```

  </template>
  <template #right>

```csharp
void fn(
  string firstName,
  string lastName
) {
  Console.WriteLine($"Hello, {firstName} {lastName}");
}

// We can pass the parameters in any order
fn (lastName: "Lee", firstName: "Amy")
```

  </template>
</CodeSplitter>

## Unbounded Parameters

<CodeSplitter>
  <template #left>

```ts
function fn() {
  for (let arg of arguments) {
    console.log(arg);
  }
}

fn("a", "b", "c");
// abc
```

  </template>
  <template #right>

```csharp
void fn(params string[] args) {
  foreach (var arg in args) {
    Console.Write(arg);
  }
}

fn("a", "b", "c");
// abc
```

  </template>
</CodeSplitter>

::: tip
C# 13 [has some nifty improvements](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#params-collections) around `params` collections.
:::

## Function Type

Like JavaScript, C# functions are first class objects represented by two system types:

1. `Action` - the equivalent of `() => void`
2. `Func<T>` - the equivalent of `() => T`

Therefore, we can freely pass around functions and lambda closures largely behave similarly to lambda closures in JavaScript (with some edge cases and specific considerations in .NET due to the multi-threaded runtime).

<CodeSplitter>
  <template #left>

```ts
// Return a function
function fn() : () => void {
  return () => {
    console.log("Here");
  }
}

fn()();

// Accept a function
function fn(
  label: string,
  fx: (name: string) => string
) : string {
  return fx(label)
}

console.log(
  fn("Steve", (name) => `Hello, ${name}`);
); // Hello, Steve
```

  </template>
  <template #right>

```csharp
// Return a function
Action fn() {
  return () => {
    Console.WriteLine("Here");
  };
}

fn()();

// Accept a function
string fn(
  string name,
  Func<string, string> fx
) {
  return fx(name);
}

Console.WriteLine(
  fn("Steve", (name) => $"Hello, {name}")
); // Hello, Steve

```

  </template>
</CodeSplitter>
