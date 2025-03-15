# JSON Serialization

In both C# and JavaScript, **JSON serialization** is the process of converting objects into JSON strings for storage, transmission, or APIs. Both languages offer built-in mechanisms for handling this, but with significant differences in type safety, performance, and functionality. In JavaScript, the `JSON` object provides the `JSON.stringify()` and `JSON.parse()` methods for serializing and deserializing data. While this is simple and works for most use cases, JavaScript's dynamic typing means there’s no compile-time checking of the object structure, and serialization can sometimes lead to runtime errors if the data doesn’t match the expected format.

In contrast, C# offers more **type safety** with its built-in `System.Text.Json` library (introduced in .NET Core 3.0) for **JSON serialization**. This library provides a fast and efficient way to serialize and deserialize objects to and from JSON, with support for strong typing, object-to-object mappings, and handling complex types like collections or custom objects. C# also offers robust support for **custom converters** if more control is needed over how specific types are serialized or deserialized. Additionally, C#'s type system allows compile-time checks to ensure that the serialized data matches the expected object structure, greatly reducing the risk of runtime errors.

## Basics

<CodeSplitter>
  <template #left>

```ts{25,29}
abstract class Vehicle {
  abstract maxSeats: number
}

class Car extends Vehicle {
  maxSeats: number;
  constructor() {
    super();
    this.maxSeats = 4;
  }
}

class Suv extends Vehicle {
  maxSeats: number;
  has3rdRow: boolean;

  constructor() {
    super();
    this.maxSeats = 6;
    this.has3rdRow = true;
  }
}

let suv = new Suv();
let json = JSON.stringify(suv)

console.log(json); // {"maxSeats":6,"has3rdRow":true}

let car = JSON.parse(json)

console.log(car) // { "maxSeats": 6, "has3rdRow": true }
```

  </template>
  <template #right>

```csharp{18,22}
using System.Text.Json;
using System.Text.Json.Serialization;

abstract record Vehicle {
  public abstract int MaxSeats { get; init; }
}

record Car : Vehicle {
  public override int MaxSeats { get; init; } = 4;
}

record Suv : Vehicle {
  public override int MaxSeats { get; init; } = 6;
  public bool Has3rdRow => true;
}

var suv = new Suv();
var json = JsonSerializer.Serialize(suv);

Console.WriteLine(json); // {"MaxSeats":6,"Has3rdRow":true}

var car = JsonSerializer.Deserialize<Car>(json);

Console.WriteLine(car); // Car { MaxSeats = 6 }
```

  </template>
</CodeSplitter>

## Constraining Serialization

Here we can see that both `JSON.parse` and `JsonSerializer.Deserialize` have the same issue by default: it's happy to accept the SUV as a Car!

In C#, we can fix this with annotations:

```csharp{1,6}
[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
record Car : Vehicle {
  public override int MaxSeats { get; init; } = 4;
}

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
record Suv : Vehicle {
  public override int MaxSeats { get; init; } = 6;
  public bool Has3rdRow => true;
}

// Error: System.Text.Json.JsonException: The JSON property 'Has3rdRow'
// could not be mapped to any .NET member contained in type 'Submission#8+Car'.
```

::: tip Manage this behavior globally
The [`JsonSerializerOptions` class](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions?view=net-9.0#properties) allows managing this more globally to prevent having to apply the `JsonUnmappedMemberHandling` on every class.  Set the [`UnmappedMemberHandling`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions.unmappedmemberhandling?view=net-9.0#system-text-json-jsonserializeroptions-unmappedmemberhandling) to `JsonUnmappedMemberHandling.Disallow`.
:::

This type of behavior can prevent issues when persisting to document-oriented databases, for example, by ensuring that mis-matches in the JSON structure to the class raise exceptions.

::: tip Use data annotations for validation
C# will already prevent many types of data quality issues out-of-the-box because it won't allow assignment of a `string` to an `int` property (JS doesn't care).  But you can also leverage [data annotations](https://dotnetfullstackdev.medium.com/new-data-annotations-in-net-8-ef5d61813596) to handle validation at the boundary.
:::

## Customizing Serialization

In C#, it is possible to tweak the serialization behavior using both global options as well as attributes.

We'll use this baseline model:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

public enum VehicleType { Car, Suv, Minivan }

public record Driver(
  string Name,
  VehicleType VehicleType
);
```

### Write Enum Labels and Lowercase Properties

By default, C# enums are represented as their numeric value.  To transmit the label and ensure our properties are `camelCase` instead of `PascalCase`, we can set the options:

```csharp{8-11,13}
var driver = new Driver("Ada", VehicleType.Suv);
var json = JsonSerializer.Serialize(driver);

Console.WriteLine(json);
// {"Name":"Ada","VehicleType":1}

// ⭐️ Write the enum label as well as lower case
var options = new JsonSerializerOptions {
  Converters = { new JsonStringEnumConverter() },
  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};

json = JsonSerializer.Serialize(driver, options);

Console.WriteLine(json);
// {"name":"Ada","vehicleType":"Suv"}
```

### Alias Property Names

```csharp{3,5}
// ⭐️ We can also change the property name in the JSON
public record NamedDriver(
  [property: JsonPropertyName("driverName")]
  string Name,
  [property: JsonPropertyName("vehicleClass")]
  VehicleType VehicleType
);

var namedDriver = new NamedDriver("Alan", VehicleType.Car);
json = JsonSerializer.Serialize(namedDriver, options);

Console.WriteLine(json);
// {"driverName":"Alan","vehicleClass":"Car"}
```

## Ignoring fields

This is a very important tool to get "free" DTO types by simply ensuring proper configuration of field and property include/excludes at serialization.  In JavaScript, you might end up defining a client Zod schema and a server Zod schema representing the same object but with fields stripped out for the client.

<CodeSplitter>
  <template #left>

```ts{19}
type VehicleType = "car" | "suv" | "minivan"

type LicensedDriver = {
    name: string,
    licenseNumber: string,
    vehicleType: VehicleType
}

let licensedDriver: LicensedDriver = {
    name: "Charles",
    licenseNumber: "12345",
    vehicleType: "minivan"
}

console.log(JSON.stringify(licensedDriver));
// {"name":"Charles","licenseNumber":"12345","vehicleType":"minivan"}

const {
    licenseNumber, // 👈 Eject the field; can't forget to do this
    ...trimmed
} = licensedDriver;

console.log(JSON.stringify(trimmed))
// {"name":"Charles","vehicleType":"minivan"}
```

  </template>
  <template #right>

```csharp{8,9}
using System.Text.Json;
using System.Text.Json.Serialization;

public enum VehicleType { Car, Suv, Minivan }

public record LicensedDriver(
  string Name,
  [property: JsonIgnore] // 👈 Erased from JSON
  string LicenseNumber,
  VehicleType VehicleType
);

var licensedDriver = new LicensedDriver(
  "Charles",
  "12345",
  VehicleType.Car
);

json = JsonSerializer.Serialize(licensedDriver, options);

Console.WriteLine(json);
// {"name":"Charles","vehicleType":"Minivan"}
```

  </template>
</CodeSplitter>

In JavaScript, you'll have to manually "eject" the field or write a transformer/mapper.  Overall, .NET's `System.Text.Json` library offers many powerful capabilities when it comes to managing serialization an deserialization of JSON compared to the built-in `JSON` utility in JavaScript.

:::tip
We'll see in [Databases and ORMs](../intermediate/databases-and-orms.md) why declarative JSON field erasure is very useful.
:::
