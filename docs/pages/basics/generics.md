# Generics

Generics in both C# and TypeScript allow developers to write reusable code by defining classes, methods, and interfaces with type parameters. This ensures type safety while maintaining flexibility, as the actual types are specified later when the code is used. In TypeScript, generics are implemented in a similar manner to C#, where you can define a class or function with placeholder types (`function identity<T>(value: T): T { return value; }`). TypeScript’s generics are fully type-checked at compile time, but TypeScript’s type system is based on structural typing, so the generics are purely for static type checking and do not affect runtime behavior.

However, C# generics come with additional performance considerations due to **boxing and unboxing** of value types (like `int`, `double`, or `struct`). When a value type is passed to a generic type in C#, it must be boxed into a reference type (object), which introduces overhead. When it’s retrieved, the value type is unboxed, which can lead to performance hits. For example, a generic collection that stores `int` values will box them into `object` at runtime, slowing down performance compared to storing them in a non-generic collection. TypeScript, on the other hand, does not have this issue, as it does not distinguish between value types and reference types at runtime—everything is treated as a reference type, avoiding boxing and unboxing entirely.

Despite this, C#'s generics provide more powerful features than TypeScript’s, including **generic constraints**, which allow developers to specify rules on the types that can be used with a generic type. This feature enables more fine-grained control over the behavior of generics in C# compared to TypeScript. Additionally, C# allows **generic methods** and **covariant/contravariant** type parameters for more advanced use cases, offering greater flexibility in designing APIs that are both type-safe and performant.

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
