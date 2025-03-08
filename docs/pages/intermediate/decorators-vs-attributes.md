# Decorators vs Attributes

TypeScript decorators and C# attributes share a similar syntax, both using the `@` (TypeScript) or `[]` (C#) notation, and they both serve as metadata-like constructs. However, their underlying mechanisms and capabilities are fundamentally different. TypeScript decorators are functions that modify class definitions at runtime, whereas C# attributes are metadata that the compiler embeds into assemblies for reflection or tooling purposes.

TypeScript decorators execute at runtime, allowing them to dynamically modify or extend class behavior, such as injecting dependencies or wrapping methods. They are primarily used in frameworks like Nest.js for declarative configuration. However, they do not influence the compilation process and cannot generate new code before execution. In contrast, C# attributes provide static metadata that can be retrieved via reflection or leveraged by Roslyn source generators to create additional code at compile time.

This makes attributes more powerful in scenarios like compile-time validation, serialization optimization, or code generation—capabilities that TypeScript decorators simply do not have. While both provide a way to annotate code, C# attributes impact both runtime and compile-time behavior, whereas TypeScript decorators only modify runtime behavior.

## Example: Module Registration

Let's use an example where we want to use decorators to label specific classes.  This is a common use case when dynamically preparing a runtime environment.

<CodeSplitter>
  <template #left>

```ts{15}
// Be sure to enable experimentalDecorators to run this example
interface Module {
  init(): void
}

// A registry
let moduleRegistry: Array<new (...args: any[]) => Module> = []

// A decorator function that will push constructors into the retistry
function ModuleInitializer<T extends new (...args: any[]) => Module>(constructor: T) {
  moduleRegistry.push(constructor)
}

// 👇 Register this module for initialization
@ModuleInitializer
class AppModule implements Module{
  init(): void {
    console.log("Initialized the app module")
  }
}

// ❌ This one isn't registered
class OtherModule implements Module{
  init(): void {
    console.log("Initialized the other module")
  }
}

for (let moduleConstructor of moduleRegistry) {
  let module = new moduleConstructor()
  module.init()
}

// "Initialized the app module"
```

  </template>
  <template #right>

```csharp{11,26}
using System.Reflection;

interface Module {
  void Init();
}

[AttributeUsage(AttributeTargets.Class)]
class ModuleInitializerAttribute : Attribute { }

// 👇 Register this module for initialization
[ModuleInitializer]
class AppModule : Module {
  public void Init() => Console.WriteLine("Initialized the app module");
}

// ❌ This one isn't registered
class OtherModule : Module {
  public void Init() => Console.WriteLine("Initialized the other module");
}

// Runtime reflection
var moduleTypes = Assembly.GetExecutingAssembly()
  .GetTypes()
  .Where(t => typeof(Module).IsAssignableFrom(t)
    && !t.IsInterface
    && t.GetCustomAttribute<ModuleInitializerAttribute>() != null
  );

foreach (var moduleType in moduleTypes) {
  var module = (Module)Activator.CreateInstance(moduleType);
  module.Init();
}

// "Initialized the app module"
```

  </template>
</CodeSplitter>

::: tip Can you spot the difference?
TC-39 decorators are *active* in that they are invoked at runtime whereas .NET attributes are *passive* meaning that on their own, they do nothing but provide metadata.  You must then write code that can process this metadata either at runtime (via reflection) or build time (via Roslyn source generators).

Attributes in C# are purely metadata.
:::

::: warning Reflection performance
It is important to note that there is a "cost" to runtime reflection and excessive use can slow down applications at startup.

Ahead-of-time (AOT) compilation requires that the codebase avoids using runtime reflection.  Instead, use attributes with source generators to dynamically create compile time code in AOT scenarios.
:::
