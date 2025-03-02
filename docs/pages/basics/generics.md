# Generics

Generics are actually quite important for performance and memory in C# because generics prevent "boxing" and "unboxing" of *value types* (like `int`, `bool`) into `object` in some operations.  (In TypeScript, they are merely a descriptor since they are no longer present at runtime).

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
