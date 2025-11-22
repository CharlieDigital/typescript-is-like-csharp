# Extension Methods

In C#, **extension methods** allow you to add methods to existing types without modifying their original implementation. These methods are defined in static classes and must take the type being extended as the first parameter, prefixed with `this`. When used, extension methods appear as if they were part of the original type, enabling developers to extend functionality in a clean and reusable way. This is particularly useful when working with third-party libraries or system types you can't modify directly.

In JavaScript, **prototype methods** serve a similar purpose by allowing developers to add methods and properties to existing objects through their prototype. This modifies the prototype chain, making the new methods available to all instances of the object. While this provides flexibility to extend built-in objects or custom objects at runtime, it can also lead to issues like unintended side effects if the extensions aren't carefully managed, since changes affect all instances of the object globally.

Both C# extension methods and JavaScript prototype methods allow extending existing functionality without altering the original codebase, but C# offers better type safety and encapsulation, while JavaScript provides more dynamic flexibility with runtime modifications.

## Basics

<CodeSplitter>
  <template #left>

```ts
// Class definition
class Person {
  constructor(
    public readonly firstName: string,
    public readonly lastName: string
  ) {}
}

// Without this, TS will complain about the `print` below.
interface Person {
  print: () => void
}

// Extend with additional methods
Person.prototype.print = function() {
  console.log(`${this.firstName} ${this.lastName}`)
}

const person = new Person("Ada", "Lovelace");
person.print(); // "Ada Lovelace"
```

  </template>
  <template #right>

```csharp
// Class definition
public record Person(
  string FirstName,
  string LastName
);

// Extend with additional methods
public static class PersonExtension {
  public static void Print(this Person person) {
    Console.WriteLine($"{person.FirstName} {person.LastName}");
  }
}

var ada = new Person("Ada", "Lovelace");
ada.Print(); // "Ada Lovelace"
```

  </template>
</CodeSplitter>

Both methods allow extending functionality on existing classes.

## Interfaces

C# goes further and also allows adding extension methods on interfaces:

```csharp{2,7,11}
// Same example, but we add an interface
public interface IContact {
  string FirstName { get; }
  string LastName { get; }
}

public record Person(string FirstName, string LastName) : IContact;

public static class PersonExtension {
  // Note the interface here instead 👇
  public static void Print(this IContact contact) {
    Console.WriteLine($"{contact.FirstName} {contact.LastName}");
  }
}

var ada = new Person("Ada", "Lovelace");
ada.Print(); // "Ada Lovelace"
```

## Extension Members

C# 14 and .NET 10 introduce [extension members](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14) as well.

This mechanism is extremely useful when working with third party types or even types from your own libraries where you may only want to extend the behavior without modifying the original.

<CodeSplitter>
  <template #left>

```ts
// Class definition
class Person {
  constructor(
    public readonly firstName: string,
    public readonly lastName: string
  ) {}
}

// Without this, TS will complain about the `displayName`
interface Person {
  displayName: string
}

// Extend with additional properties
Object.defineProperty(Person.prototype, "displayName", {
  get() {
    return `${this.firstName} ${this.lastName}`;
  },
});

const person = new Person("Ada", "Lovelace");
console.log(person.displayName) // "Ada Lovelace"
```

  </template>
  <template #right>

```csharp{2,11-14}
var contact = new Contact("Didi", "Cheng");
Console.WriteLine(contact.DisplayName);

public record Contact(
  string FirstName,
  string LastName
);

// 👇 Define one or more extension members here
public static class ContactExtensions {
  extension(IContact contact) {
    public string DisplayName =>
      $"{contact.FirstName} {contact.LastName}";
  }
}
```

  </template>
</CodeSplitter>

One additional note is that in some cases, you may want to only add the extension members in the context of a single file.  This can be useful when you need to add some utility members, but you do not want to pollute the general namespace.

```cs
// 👇 Note the use of `file` here
file static class ContactExtensions {
  extension(IContact contact) {
    public string DisplayName =>
      $"{contact.FirstName} {contact.LastName}";
  }
}
```

Not we can add this member only in the scope of the file.
