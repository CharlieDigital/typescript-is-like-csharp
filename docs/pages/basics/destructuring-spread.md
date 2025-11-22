# Destructuring and Spread Operations

C# supports [a limited set of destructuring](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct) and spread operations compared to JS and while generally useful, are not as powerful (and potentially dangerous?) as JS destructuring and spread.

In JavaScript, destructuring and spread a commonly used to perform object shape manipulations and transformations while in C#, they are more restricted in that sense and are more basic operations.

Let's take a look.

## Destructuring Assignment

Both languages allow destructuring assignment:

<CodeSplitter>
  <template #left>

```ts
type Person = {
  firstName: string
  lastName: string
}

const ada: Person = {
  firstName: "Ada",
  lastName: "Lovelace"
}

let { firstName, lastName } = ada;
// 👇 Rename requires explicit reassignment
let { firstName: first, lastName: last } = ada;

console.log(firstName); // Ada
console.log(lastName); // Lovelace

console.log(first); // Ada
console.log(last); // Lovelace
```

  </template>
  <template #right>

```cs
public record Person(
  string FirstName,
  string LastName
);

var person = new Person(
  "Ada",
  "Lovelace"
);

var (first, last) = person;

Console.WriteLine(first); // Ada
Console.WriteLine(last); // Lovelace
```

  </template>
</CodeSplitter>

Note a key difference here: in C#, it is possible to rename the properties on assignment.

Another key difference is that C# deconstruct is "all or nothing."


<CodeSplitter>
  <template #left>

```ts
// Valid to just take `firstName`
let { firstName: fn } = ada;
```

  </template>
  <template #right>

```cs
// ❌ Not valid
var (fn) = person;

// ✅ Valid using a "discard"
var (fn, _) = person;
```

  </template>
</CodeSplitter>

That second parameter `_` on the C# side is called a "[discard](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards)".

### Adding Destructuring to C# Classes

C# destructuring is by only "free" with C# `Tuple` and `record` types (for `record` types, it is only for the positional properties declared in the constructor).

For other types, it is necessary to add it manually:

```cs{5-11}
public class Person { // 👈 Note: this is a `class`, not a `record`
  public string FirstName { get; set; }
  public string LastName { get; set; }
  // 👇 Manually added method using `out` parameters
  public void Deconstruct(
    out string firstName,
    out string lastName
  ) {
    firstName = FirstName;
    lastName = LastName;
  }
}

var ada = new Person {
  FirstName = "Ada",
  LastName = "Lovelace"
};

var (firstName, lastName) = ada;

Console.WriteLine(firstName); // Ada
Console.WriteLine(lastName); // Lovelace
```

## Spread

Spread is a double-edged sword in JS, but is a powerful tool, nonetheless and C# only offers a small subset of the capabilities and only with collections.

### Collections

Both C# and JS support spread operations on collections:

<CodeSplitter>
  <template #left>

```ts
let mine = ["apple", "banana"];
let yours = ["orange", "grape"];

let ours = [...mine, ...yours];
console.log(ours); // ["apple","banana","orange","grape"]
```

  </template>
  <template #right>

```cs
var mine = new [] { "apple", "banana" };
var yours = new [] { "orange", "grape" };

string[] ours = [.. mine, .. yours];
Console.WriteLine(string.Join(", ", ours)); // "apple, banana, orange, grape"
```

  </template>

</CodeSplitter>

In C#, the `[.. enumerable]` is useful as shorthand when working with `Linq` as well (shorthand to materialize the list).

### Objects

JS takes this a step further and allows _objects_ to be spread.

<CodeSplitter>
  <template #left>

```ts
let ada = {
  firstName: "Ada",
  lastName: "Lovelace"
};

let charles = {
  firstName: "Charles",
  lastName: "Babbage",
  nickName: "Chuck"
};

let adaClone = { ...ada };
console.log(adaClone); // { firstName: "Ada", lastName: "Lovelace" }

let who = { ...ada, ...charles };
console.log(who); // { firstName: "Charles", lastName: "Babbage", nickName: "Chuck" }}
```

  </template>
  <template #right>

```cs
// No equivalent; need to manipulate the objects
var ada = new {
  FirstName = "Ada",
  LastName = "Lovelace"
};

var charles = new {
  FirstName = "Charles",
  LastName = "Babbage",
  Nickname = "Chuck"
};

var adaClone = new {
  ada.FirstName,
  ada.LastName,
};

Console.WriteLine(adaClone); // { FirstName = Ada, LastName = Lovelace }

var who = new {
  ada.FirstName,
  ada.LastName,
  charles.Nickname
};

Console.WriteLine(who); // { FirstName = Ada, LastName = Lovelace, Nickname = Chuck }

```

  </template>

</CodeSplitter>

::: info Why double-edged?
There are a few reasons why I generally avoid spread operators with objects in JS.  First is that it can make the real shape "opaque" and becomes a slippery slope in TypeScript where it can become very difficult to track down where a field is coming from.  Second is that it is easy to override existing fields by accident.  I generally use it sparingly for objects (usually when cloning) as I find it has the quality of making code harder to skim and comprehend since it then often requires an additional mental "stack push" to understand an underlying shape.
:::
