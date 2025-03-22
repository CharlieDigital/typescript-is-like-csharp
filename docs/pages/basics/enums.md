# Enums

Both TypeScript and C# use `enum`s as a way to represent a fixed set of named constants, but their implementations and reliability differ significantly. In C#, `enum`s are strongly typed, compile to efficient integer values, and integrate seamlessly with the type system, providing strict compile-time checks. This ensures that only valid `enum` values can be assigned, making `enum`s safe and predictable to use, especially in large codebases.

Many developers avoid using TypeScript `enum` types because they introduce unnecessary complexity and runtime overhead. Unlike other TypeScript types, `enum`s emit additional JavaScript code, which can bloat bundles and complicate tree-shaking. They also behave inconsistently compared to simpler alternatives like `const` objects or union string literals, which offer better type safety, clearer autocompletion, and no extra runtime footprint. As a result, many developers prefer these lighter, more predictable patterns over `enum`s.

## Basics

<CodeSplitter>
  <template #left>

```ts
// Using default values
enum Status {
  default,
  queued,
  processing,
  completed,
  success,
  error
}

// Using explicit values
enum Status {
  default = 5,
  queued = 10,
  processing = 15,
  completed = 20,
  success = 25
  error = 30
}
```

  </template>
  <template #right>

```csharp
// Using default values
enum Status {
  Default,
  Queued,
  Processing,
  Completed,
  Success,
  Error
}

// Using explicit values
enum Status {
  Default = 5,
  Queued = 10,
  Processing = 15,
  Completed = 20,
  Success = 25,
  Error = 30
}
```

  </template>
</CodeSplitter>

TypeScript produces the following artifact:

```typescript
var Status;
(function (Status) {
    Status[Status["default"] = 5] = "default";
    Status[Status["queued"] = 10] = "queued";
    Status[Status["processing"] = 15] = "processing";
    Status[Status["completed"] = 20] = "completed";
    Status[Status["error"] = 25] = "error";
})(Status || (Status = {}));
```

## Use as Flags in C#

In C# [enums can be used as *flags* as well](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#enumeration-types-as-bit-flags).

For example;

```csharp
enum Status {
  Default = 0,
  Queued = 1,
  Processing = 2,
  Completed = 4,
  Success = 8,
  Error = 16
}

var completedSuccessfully = Status.Completed | Status.Success;

if ((completedSuccessfully & Status.Success) == Status.Success) {
  Console.WriteLine("Completed successfully");
} // "Completed successfully"

var completedWithError = Status.Completed | Status.Error;

if ((completedWithError & Status.Error) == Status.Error) {
  Console.WriteLine("Completed with error");
} // "Completed with errors"
```

Bit flags like this are an efficient way to store values like permissions and statuses while still being easy to use and accessible because of the `enum` type.

## Serialization in C#

C# `enum`s generally translate into TypeScript string literal unions when generated from an OpenAPI schema so they are safe to use.

For serialization purposes, the `JsonStringEnumConverter` should be configured to output strings if that is preferred.

```csharp
using System.Text.Json;
using System.Text.Json.Serialization;

enum Status {
  Default = 0,
  Queued = 1,
  Processing = 2,
  Completed = 4,
  Success = 8,
  Error = 16
}

record Job(string Name, Status Status);

var options = new JsonSerializerOptions {
  Converters = { new JsonStringEnumConverter() }
};

var job1 = new Job("job1", Status.Completed);

Console.WriteLine(JsonSerializer.Serialize(job1));
// {"Name":"job1","Status":4}

Console.WriteLine(JsonSerializer.Serialize(job1, options));
// {"Name":"job1","Status":"Completed"}
```

Serialization can also be [customized using attributes](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/customize-properties#custom-enum-member-names).
