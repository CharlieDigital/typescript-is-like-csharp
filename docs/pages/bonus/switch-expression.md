# Switch Expressions

C# **switch expressions** (introduced in C# 8.0) provide a more concise and readable way to handle multiple conditions compared to traditional `switch-case` statements. Unlike the classic `switch`, where each case block is followed by a `break` statement, the switch expression evaluates to a value and can be used directly in assignments or return statements. This makes it ideal for cases where you need to map a value to another value or execute simple logic, all in a compact and readable format. The syntax is clean, reducing the need for boilerplate code and improving the clarity of complex decision trees.

One of the key advantages of switch expressions is **exhaustiveness checking**. The compiler can ensure that all possible cases are handled, reducing the risk of runtime errors caused by missing conditions. If a case is missing, the compiler will flag it, enforcing correctness and preventing unintended behavior. This is especially helpful when dealing with enums or discriminated unions, where each possible value must be accounted for. Additionally, switch expressions support pattern matching, allowing for more powerful and flexible condition checking, further enhancing readability and correctness in handling complex data types.

## Exhaustive Checks

One of the best features of switch expressions in C# is exhaustive type checks.  We can emulate this to some extent using TypeScript `Record`s:

<CodeSplitter>
  <template #left>

```ts
type VehicleOption = "car" | "suv" | "minivan"

abstract class Vehicle {}
class Car extends Vehicle { }
class Suv extends Vehicle { }
class Minivan extends Vehicle { }

// Without the type, the last line requires a cast.
let request: {
  vehicle: VehicleOption,
  passengerCount: number
} = {
  vehicle: "suv",
  passengerCount: 5
};

let vehicles: Record<VehicleOption, Vehicle> = {
  car: new Car(),
  suv: new Suv(),
  minivan: new Minivan()
}

let vehicle = vehicles[request.vehicle]
```

  </template>
  <template #right>

```csharp
enum VehicleOption { Car, Suv, Minivan }

abstract class Vehicle();
class Car : Vehicle { }
class Suv : Vehicle { }
class Minivan : Vehicle { }

var request = new {
  Vehicle = VehicleOption.Suv,
  PassengerCount = 5
};

// Pattern matching and exhaustiveness as a warning (default) or error (configure in .csproj)
Vehicle vehicle = request switch {
  { Vehicle: VehicleOption.Car } => new Car(),
  { Vehicle: VehicleOption.Suv } => new Suv(),
  { Vehicle: VehicleOption.Minivan } => new Minivan(),
  _ => throw new ArgumentException("Invalid vehicle option")
};

// We can also do it this way:
Vehicle vehicle2 = request.Vehicle switch {
  VehicleOption.Car => new Car(),
  VehicleOption.Suv => new Suv(),
  VehicleOption.Minivan => new Minivan(),
  _ => throw new ArgumentException("Invalid vehicle option")
};
```

  </template>
</CodeSplitter>

## Pattern Matching

[Tim Deschryver](https://timdeschryver.dev/blog/pattern-matching-examples-in-csharp) has the absolute best overview of the extensive pattern matching capabilities that can be used with switch expressions in C#.

Switching on types, for example, is just one of the many really useful features of pattern matching in C#.

<CodeSplitter>
  <template #left>

```ts
// Continue from our example above
let label = ""

switch(vehicle.constructor.name) {
  case Car.name: label = "Your car is arriving soon..."; break;
  case Suv.name: label = "Your SUV is arriving soon..."; break;
  case Minivan.name: label = "Your minival is arriving soon..."; break;
  default: label = "Uh oh! We can't find your vehicle"; break;
} // "Your SUV is arriving soon..."
```

  </template>
  <template #right>

```csharp
// Continue from our example above
var label = vehicle switch {
  Car => "Your car is arriving soon...",
  Suv => "Your SUV is arriving soon...",
  Minivan => "Your minivan is arriving soon...",
  _ => "Uh oh! We can't find your vehicle"
}; // "Your SUV is arriving soon..."

// Using numeric patterns matching on a Tuple
Vehicle vehicle3 = (Passengers: 5, Luggage: 6) switch {
  (> 6, > 8) => new Minivan(),
  (> 4, > 4) => new Suv(),
  _ => new Car()
}; // Suv
```

  </template>
</CodeSplitter>
