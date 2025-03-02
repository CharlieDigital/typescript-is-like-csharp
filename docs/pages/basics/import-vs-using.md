# `import` vs `using`

In C#, the `using` keyword is used to import namespaces, which are collections of related types such as classes, interfaces, and enums. By importing a namespace with `using`, you can directly access its types without needing to fully qualify their names every time. For example, `using System;` allows you to use types like `Console.WriteLine()` without needing to write `System.Console.WriteLine()`. This helps keep C# code clean and concise. The `using` statement is typically placed at the top of a file and allows for easy reference to namespaces across the entire file.

In TypeScript and JavaScript, the `import` keyword is used to bring in modules, typically from external files or packages. JavaScript and TypeScript organize code into **modules**, and the `import` statement allows you to selectively bring in specific functions, classes, or objects from these modules. For example, `import { myFunction } from './myModule';` imports a named export from a file. TypeScript’s `import` works similarly to JavaScript’s ES6 `import` and offers the added benefit of static type checking.

## Basics

<CodeSplitter>
  <template #left>

```ts
// Named import
import { ref } from "vue";

// Namespace import
import * as fs from "fs";

// Default import
import tool from "package";
```

  </template>
  <template #right>

```csharp
// Namespace import
using System.Text;

// Aliased namespace import
using Txt = System.Text;

// Import the static members so they can be used without a namespace.
using static System.Math;
```

  </template>
</CodeSplitter>

::: tip
`using` has two main use cases in C#.  The first is as we've seen here where we are *importing* and *using* classes and functions from a namespace.

The second use case is to [instruct a `disposable` object to clean up resources](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) as its active lifecycle has ended.  This second use case [is the same as TypeScript 5.2's `using`](https://www.totaltypescript.com/typescript-5-2-new-keyword-using) that implements [TC-39's proposal for resource management](https://github.com/tc39/proposal-explicit-resource-management#definitions).

Can you see just how similar these languages are and how C# and TS influence each other?
:::

::: info
The third use case for `using` is actually to define a file-scoped named tuple type, which we'll also explore later.
:::

## Multi-File

Whereas JavaScript modules are imported based on file path, .NET namespaces are imported based on name.  So multiple files can belong to the same namespace and be imported together without any extra work.  In JavaScript, we need to aggregate multiple modules into one or add them one by one.

<CodeSplitter>
  <template #left>

```ts{7,14,15}
// address.ts
export class Address {

}

// contact.ts
import { Address } from "./address";

export class Contact {
  constructor(public address: Address) { }
}

// app.ts
import { Address } from "./address";
import { Contact } from "./contact";

class App {
  getContacts() : Contact[] {
    return [];
  }

  getAddresses(): Address[] {
    return [];
  }
}

```

  </template>
  <template #right>

```csharp{2,9,17,19}
// Address.cs
namespace ChrlsChn.Crm.Models;

class Address {

}

// Contact.cs
namespace ChrlsChn.Crm.Models;

class Contact {
  // No need to import since these are in the same namespace
  public Address Address { get; set; }
}

// App.cs
using ChrlsChn.Crm.Models; // Import the namespace

namespace ChrlsChn.Crm;

class App {
  public List<Contact> GetContacts() { /*... */ }
  public List<Address> GetAddresses() { /*... */ }
}
```

  </template>
</CodeSplitter>

::: tip
In .NET, there's no need to match your namespace to your directory structure, but it can be useful to help make code more navigable.  There is overall a lot more freedom to organize namespaces compared to modules and a bit less work.

With JS, you may commonly end up with an `index.ts` that's just filled with exports for a given directory.
:::
