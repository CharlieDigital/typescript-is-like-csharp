# Classes and Types

Classes in C# and TypeScript/JavaScript share the same basic structure, allowing the creation of blueprints for objects with properties, methods, and constructors. However, C# classes come with stronger typing, more powerful features, and stricter rules. C# uses `class` to define a class, and constructors are defined using the class name. The class members (fields, properties, methods) are explicitly typed, offering better compile-time safety compared to TypeScript, where type annotations are optional and inferred.

C# **generics** allow classes and methods to be defined with type parameters (`class Box<T> {}`), providing a highly type-safe way to handle different data types. While TypeScript also supports generics, C#'s implementation is more robust with additional constraints, making it easier to enforce type safety and reduce errors.

In C#, **primary constructors** are a concise feature where you can define parameters directly in the class declaration, eliminating the need for a separate constructor body (`class Person(string name) { }`). TypeScript lacks a direct equivalent but can mimic this with class properties destructured in the constructor (see below).

Additionally, C#'s **record classes**, are immutable reference types designed for storing data with value-based equality. Record classes (`public record Person(string Name, int Age);`) automatically generate equality methods, `ToString()`, and `Clone()`, reducing boilerplate code. TypeScript can approximate this behavior with classes or interfaces, but it lacks built-in support for value-based equality and immutability, making C#'s record types a unique feature for modeling immutable data.

## Basic Classes

<CodeSplitter>
  <template #left>

```ts
class Person {
  static Type = "person"

  constructor(
    private firstName: string,
    private lastName: string
  ) { }

  get displayName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  notify() {
    console.log(`Notified ${Person.Type}: ${this.displayName}`);
  }
}

let frankie = new Person("Frank", "Sinatra");
frankie.notify(); // "Notified person: Frank Sinatra"
```

  </template>
  <template #right>

```csharp{2-3,5,8}
class Person(
  string firstName,
  string lastName
) {
  public string DisplayName => $"{firstName} {lastName}";

  public void Notify() {
    Console.WriteLine($"Notified {nameof(Person)}: {DisplayName}");
  }
}

var frankie = new Person("Frank", "Sinatra");
frankie.Notify(); // "Notified Person: Frank Sinatra"
```

  </template>
</CodeSplitter>

::: tip
In C#, `this` is optional; it is not necessary to use it inside of the scope of a class to reference class variables.  You can choose to do so for clarity, but it is not required.
:::

In this C# example, are a few things to call out:

- We use a **primary constructor** to define the class.  This allows the class definition to accept the parameters which are treated as private fields.
- Property accessors like `DisplayName` can be defined using lambda expressions.
- Because C# still has the type metadata at runtime, we can use `nameof(Person)` here at runtime.

## Inheriting Classes

<CodeSplitter>
  <template #left>

```ts
class MobileDevice {
  call(recipient: number) {
    console.log(`Calling: ${recipient}`);
  }
}

class AndroidPhone extends MobileDevice { }

class ApplePhone extends MobileDevice { }

let pixel = new AndroidPhone();
pixel.call(1234567); // "Calling: 1234567"

let iphone = new ApplePhone();
iphone.call(1234567); // "Calling: 1234567"
```

  </template>
  <template #right>

```csharp
class MobileDevice {
  public void Call(int recipient) {
    Console.WriteLine($"Calling: {recipient}");
  }
}

class AndroidPhone : MobileDevice { }

class ApplePhone : MobileDevice { }

var pixel = new AndroidPhone();
pixel.Call(1234567); // "Calling: 1234567"

var iphone = new ApplePhone();
iphone.Call(1234567); // "Calling: 1234567"
```

  </template>
</CodeSplitter>

## Interfaces

<CodeSplitter>
  <template #left>

```ts{1}
interface IMobileDevice {
  call: (recipient: number) => void;
}

class AndroidPhone implements IMobileDevice {
  call(recipient: number) {
    console.log(`Calling ${recipient} from my Android device...`);
  }
}

class ApplePhone implements IMobileDevice {
  call(recipient: number) {
    console.log(`Calling ${recipient} from my Apple device...`);
  }
}

let pixel = new AndroidPhone();
pixel.call(1234567); // "Calling 1234567 from my Android device"

let iphone = new ApplePhone();
iphone.call(1234567); // "Calling 1234567 from my Apple device"
```

  </template>
  <template #right>

```csharp{1}
interface IMobileDevice {
  void Call(int recipient);
}

class AndroidPhone : IMobileDevice {
  public void Call(int recipient) {
    Console.WriteLine($"Calling {recipient} from my Android device...");
  }
}

class ApplePhone : IMobileDevice {
  public void Call(int recipient) {
    Console.WriteLine($"Calling {recipient} from my Apple device...");
  }
}

var pixel = new AndroidPhone();
pixel.Call(1234567); // "Calling 1234567 from my Android device"

var iphone = new ApplePhone();
iphone.Call(1234567); // "Calling 1234567 from my Apple device"
```

  </template>
</CodeSplitter>

## Abstract Classes

<CodeSplitter>
  <template #left>

```ts{1,6,8}
abstract class MobileDevice {
  call(recipient: number) {
    console.log(`Calling: ${recipient}`);
  }

  abstract powerOn(): void;

  connectCable() {
    console.log("Connecting USB-C...");
  }
}

class AndroidPhone extends MobileDevice {
  powerOn() {
    console.log("Powering Android device on");
  }
}

class ApplePhone extends MobileDevice {
  constructor(private version: number) { super(); }

  powerOn() {
    console.log("Powering Apple device on");
  }

  override connectCable() {
    if (this.version < 15) {
      console.log("Connecting Lightning cable...");
    } else {
      console.log("Connecting USB-C...");
    }
  }
}

let pixel = new AndroidPhone();
pixel.powerOn(); // "Powering Android device on"
pixel.connectCable(); // "Connecting USB-C..."

let iphone = new ApplePhone(14);
iphone.powerOn(); // "Powering Apple device on"
pixel.connectCable(); // "Connecting Lightning cable..."
```

  </template>
  <template #right>

```csharp{1,6,8}
abstract class MobileDevice {
  public void Call(int recipient) {
    Console.WriteLine($"Calling: {recipient}");
  }

  public abstract void PowerOn();

  public virtual void ConnectCable() {
    Console.WriteLine("Connecting USB-C...");
  }
}

class AndroidPhone : MobileDevice {
  public override void PowerOn() {
    Console.WriteLine("Powering Android device on...");
  }
}

class ApplePhone(int version) : MobileDevice {
  // 👆 Using primary constructor

  public override void PowerOn() {
    Console.WriteLine("Powering Apple device on...");
  }

  public override void ConnectCable() {
    if (version < 15) {
      Console.WriteLine("Connecting Lightning cable...");
    } else {
      Console.WriteLine("Connecting USB-C...");
    }
  }
}

var pixel = new AndroidPhone();
pixel.PowerOn(); // "Powering Android device on"
pixel.ConnectCable(); // "Connecting USB-C..."

var iphone = new ApplePhone(14);
iphone.PowerOn(); // "Powering Apple device on"
iphone.ConnectCable(); // "Connecting Lighting cable..."
```

  </template>
</CodeSplitter>

## Record Classes

C# [record classes](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) provide immutability semantics to classes (we won't cover `record struct` here).

```csharp
record Contact(
  string FirstName,
  string LastName
) {
  public string DisplayName => $"{FirstName} {LastName}";
};

var alan = new Contact("Alan", "Turing");
alan.FirstName = "Allen"; // ❌ Error: cannot modify a record
alan = alan with { FirstName = "Al" }; // OK
var (FirstName, LastName) = alan; // Deconstructing a record
Console.WriteLine(alan.DisplayName); // "Al Turing"

var al = new Contact("Al", "Turing");
Console.WriteLine(al == alan); // True
```

Notice that last line: these two objects are equal because records follow value equality rules.

::: warning
Record classes have some limitations when it comes to working with key framework components like the Entity Framework ORM.  It is recommended to use record types when working with truly immutable data and not in cases where you're working with database records.
:::

## Anonymous Types

This perhaps belongs in another section of the document, but it'll make more sense here!  C# has a concept of an [**anonymous type**](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/anonymous-types) which is only "shaped" in the context of a scope like a function.  Outside of the function, it appears as an `object` and isn't terribly useful!

They are good for modelling data and used extensively by LINQ and in many ways resemble `Record` types in TypeScript.

<CodeSplitter>
  <template #left>

```ts
let contact = {
  firstName: "Charles",
  lastName: "Babbage"
};

console.log(contact.firstName); // "Charles"
```

  </template>
  <template #right>

```csharp
var contact = new {
  FirstName = "Charles",
  LastName = "Babbage"
};

Console.WriteLine(contact.FirstName); // "Charles"
```

  </template>
</CodeSplitter>

We'll encounter these again in [LINQ](../intermediate/linq.md).

## Class Detection

<CodeSplitter>
  <template #left>

```ts
class MobileDevice { }
class AndroidPhone extends MobileDevice { }
class ApplePhone extends MobileDevice { }

let pixel = new AndroidPhone();
let iphone = new ApplePhone();

console.log(pixel instanceof AndroidPhone); // true
console.log(pixel instanceof MobileDevice); // true
```

  </template>
  <template #right>

```csharp
class MobileDevice { }
class AndroidPhone : MobileDevice { }
class ApplePhone : MobileDevice { }

var pixel = new AndroidPhone();
var iphone = new ApplePhone();

Console.WriteLine(pixel is AndroidPhone); // True
Console.WriteLine(pixel is MobileDevice); // True
```

  </template>
</CodeSplitter>

Unlike TypeScript *types*, JavaScript *classes* do not disappear at runtime so we can still use it to discriminate the instance type.

In C#, we can also use types with pattern matching and switch expressions:

```csharp{12-17}
class MobileDevice { }
class AndroidPhone : MobileDevice { }
class ApplePhone(int version) : MobileDevice {
  public int Version { get; } = version;
}

var pixel = new AndroidPhone();
var iphone16 = new ApplePhone(16);
var iphone14 = new ApplePhone(14);

void CheckDevice(MobileDevice device) {
  var message = device switch {
    AndroidPhone => "This is an Android phone",
    ApplePhone and { Version: >= 15 } => "This is an Apple phone with USB-C",
    ApplePhone => "This is an Apple phone with Lightning",
    _ => "Mobile device"
  };

  Console.WriteLine(message);
}

CheckDevice(pixel); // "This is an Android phone"
CheckDevice(iphone16); // "This is an Apple phone with USB-C"
CheckDevice(iphone14); // "This is an Apple phone with Lightning"
```

This powerful feature of C# allows us to write expressive yet eminently readable code.

::: info
In the section on reflection, we'll explore how we can use type metadata at runtime in C#.
:::

## Type Unions

C# currently does not have native type unions ([though it's somewhere on the roadmap](https://github.com/dotnet/csharplang/blob/main/proposals/TypeUnions.md)).  Of course, TypeScript's superpower is its powerful type system at dev and build time (unfortunately, it means nothing at runtime).

To get type unions, two packages can be used:

- [OneOf](https://github.com/mcintyre321/OneOf)
- [dunet](https://github.com/domn1995/dunet)

<CodeSplitter>
  <template #left>

```ts
function chooseTransit(
  numPeople: number
) : TransitOption {
  if (numPeople === 1) return { electric: false }
  if (numPeople < 5) return { numSeats: 5 }
  if (numPeople < 7) return { numSeats: 8 }
  else return { type: 'bullet' }
}

type TrainType = 'bullet' | 'normal'

type Car = { numSeats: number }
type Scooter = { electric: boolean }
type Train = { type: TrainType }

type TransitOption = Car | Scooter | Train
```

  </template>
  <template #right>

```csharp
Transit.TransitOption ChooseTransit(
  int numPeople
) {
  if (numPeople == 1) return new Scooter(true);
  if (numPeople < 5) return new Car(5);
  if (numPeople < 7) return new Car(8);
  return new Train(TrainType.Bullet);
}

enum TrainType { Bullet, Normal }

record Car(int numSeats);
record Scooter(bool electric);
record Train(TrainType type);

namespace Transit { // OneOf requires a namespace
  [GenerateOneOf]
  partial class TransitOption : OneOfBase<Car, Scooter, Train> { };
}
```

  </template>
</CodeSplitter>

A key difference is that the result of this function call at runtime ***still carries the type information*** in C#.  So we can use this information with switch expressions:

```csharp
var log = (object msg) => Console.WriteLine(msg);

ChooseTransit(5)
  .Switch(
    car => log($"Car with {car.numSeats} seats"),
    scooter => log($"Scooter is electric: {scooter.electric}"),
    train => log($"Train type: {train.type}")
  );

// "Car with 8 seats"
```
