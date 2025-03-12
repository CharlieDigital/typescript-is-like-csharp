# Generics

Generics in both C# and TypeScript allow developers to write reusable code by defining classes, methods, and interfaces with type parameters. This ensures type safety while maintaining flexibility, as the actual types are specified later when the code is used. In TypeScript, generics use a similar syntax to C#, where you can define a class or function with placeholder types (`function identity<T>(value: T): T { return value; }`). TypeScript’s generics are fully "shape-checked" *at compile time* as TypeScript’s type system is based on structural typing. The generics are purely for static type checking and do not affect runtime behavior; at runtime nothing prevents a TypeScript `Array<User>` (now just a JavaScript `Array`) from accepting a `string` value.

In C# generics come with additional performance considerations as it prevents **boxing and unboxing** of value types (like `int`, `double`, or `struct`). When a value type is passed to a generic type in C#, it does not need to be "boxed" into a reference type (object), which introduces overhead. Without generics, value types are "unboxed" on retrieval from the collection which can lead to performance hits. For example, a non-generic collection that stores `int` values will box them into `object` at runtime, slowing down performance compared to storing them in a generic collection.  C# generics maintain their generic type metadata at runtime and therefore, continue to enforce the type constraint; an exception will be thrown if a `string` is added to a  `List<int>`.

## Generic Classes

<CodeSplitter>
  <template #left>

```ts{13,14}
class MobileDevice {
  reboot() {
    console.log("Rebooting the device...");
  }
}

class AndroidPhone extends MobileDevice { }
class ApplePhone extends MobileDevice { }

let pixel = new AndroidPhone();
let iphone = new ApplePhone();

class Fixer<TDevice extends MobileDevice> {
  constructor(private device: TDevice) { }

  fix() {
    this.device.reboot();
  }
}

var fixer = new Fixer(pixel);
fixer.fix(); // "Rebooting the device...";
```

  </template>
  <template #right>

```csharp{13,14}
class MobileDevice {
  public void Reboot() {
    Console.WriteLine("Rebooting the device...");
  }
}

class AndroidPhone : MobileDevice { }
class ApplePhone : MobileDevice { }

var pixel = new AndroidPhone();
var iphone = new ApplePhone();

class Fixer<TDevice>(TDevice device)
  where TDevice : MobileDevice {

  public void Fix() {
    device.Reboot();
  }
}

var fixer = new Fixer<MobileDevice>(pixel);
fixer.Fix(); // "Rebooting the device..."
```

  </template>
</CodeSplitter>

## Generic Functions

<CodeSplitter>
  <template #left>

```ts
class Fixer {
  fix<TDevice extends MobileDevice>(device: TDevice) {
    device.reboot();
  }
}

var fixer = new Fixer();
fixer.fix(pixel);
```

  </template>
  <template #right>

```csharp
class Fixer {
  public void Fix<TDevice>(TDevice device)
    where TDevice : MobileDevice {
    device.Reboot();
  }
}

var fixer = new Fixer();
fixer.Fix(pixel); // "Rebooting the device..."
```

  </template>
</CodeSplitter>
