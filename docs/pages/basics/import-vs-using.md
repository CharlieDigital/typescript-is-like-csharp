# `import` vs `using`

In C#, the `using` keyword is used to import namespaces, which are collections of related types such as classes, interfaces, and enums. By importing a namespace with `using`, you can directly access its types without needing to fully qualify their names every time. For example, `using System;` allows you to use types like `Console.WriteLine()` without needing to write `System.Console.WriteLine()`. This helps keep C# code clean and concise. The `using` statement is typically placed at the top of a file and allows for easy reference to namespaces across the entire file.

In TypeScript and JavaScript, the `import` keyword is used to bring in modules, typically from external files or packages. JavaScript and TypeScript organize code into **modules**, and the `import` statement allows you to selectively bring in specific functions, classes, or objects from these modules. For example, `import { myFunction } from './myModule';` imports a named export from a file. TypeScript’s `import` works similarly to JavaScript’s ES6 `import` and offers the added benefit of static type checking.

It is important to note the distinction here that C# namespaces are *virtualized* paths meaning you can assign any file in any path to any namespace if it makes sense to put them into the same namespace.  On the other hand, JavaScript modules are path based meaning that there are some more challenges when there is a need to coalesce related artifacts together into one module.

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

## Ergonomics

The ergonomics of using namespaces in C# is probably better than using file paths as module scopes.  In JS and TS, it is common to see code like this from the [Cal.com repo](https://github.com/calcom/cal.com/blob/main/apps/api/v2/src/modules/organizations/attributes/options/organizations-attributes-options.controller.ts):

```js
import { API_VERSIONS_VALUES } from "@/lib/api-versions";
import { PlatformPlan } from "@/modules/auth/decorators/billing/platform-plan.decorator";
import { Roles } from "@/modules/auth/decorators/roles/roles.decorator";
import { ApiAuthGuard } from "@/modules/auth/guards/api-auth/api-auth.guard";
import { PlatformPlanGuard } from "@/modules/auth/guards/billing/platform-plan.guard";
import { IsAdminAPIEnabledGuard } from "@/modules/auth/guards/organizations/is-admin-api-enabled.guard";
import { IsOrgGuard } from "@/modules/auth/guards/organizations/is-org.guard";
import { RolesGuard } from "@/modules/auth/guards/roles/roles.guard";
import { CreateOrganizationAttributeOptionInput } from "@/modules/organizations/attributes/options/inputs/create-organization-attribute-option.input";
import { AssignOrganizationAttributeOptionToUserInput } from "@/modules/organizations/attributes/options/inputs/organizations-attributes-options-assign.input";
import { UpdateOrganizationAttributeOptionInput } from "@/modules/organizations/attributes/options/inputs/update-organizaiton-attribute-option.input.ts";
import {
  AssignOptionUserOutput,
  UnassignOptionUserOutput,
} from "@/modules/organizations/attributes/options/outputs/assign-option-user.output";
import { CreateAttributeOptionOutput } from "@/modules/organizations/attributes/options/outputs/create-option.output";
import { DeleteAttributeOptionOutput } from "@/modules/organizations/attributes/options/outputs/delete-option.output";
import { GetOptionUserOutput } from "@/modules/organizations/attributes/options/outputs/get-option-user.output";
import { GetAllAttributeOptionOutput } from "@/modules/organizations/attributes/options/outputs/get-option.output";
import { UpdateAttributeOptionOutput } from "@/modules/organizations/attributes/options/outputs/update-option.output";
import { OrganizationAttributeOptionService } from "@/modules/organizations/attributes/options/services/organization-attributes-option.service";
import { Body, Controller, Delete, Get, Param, ParseIntPipe, Patch, Post, UseGuards } from "@nestjs/common";
import { ApiOperation, ApiTags as DocsTags } from "@nestjs/swagger";
```

And having to use "barrel files" [like this one](https://github.com/calcom/cal.com/blob/main/packages/lib/index.ts) to export multiple sub-modules as a single module:

```js
export { default as isPrismaObj, isPrismaObjOrUndefined } from "./isPrismaObj";
export * from "./isRecurringEvent";
export * from "./isEventTypeColor";
export * from "./schedules";
export * from "./event-types";
```

Overall, C#'s namespaces feel more ergonomic and their decoupling from file paths makes them easier to abstract collections of related entities without having to conform your physical file organization structure.

C#'s [global using](../bonus/global-usings.md) makes this even easier and works like the [`unjs/unimport` package](https://github.com/unjs/unimport) to provide global imports and clean up messy module imports in JS/TS.

::: tip JavaScript module loading notes
JavaScript's module loading could be seen as optimized for browser-based scenarios where it can be advantageous to load files only on demand to deliver smaller payloads across the wire.  In a server-based environment, it can still be useful for serverless scenarios optimized for startup speed.

.NET's runtime `AppDomain` also only loads assemblies on demand as referenced classes are used (except in ahead of time compilation scenarios), but optimizing for this behavior by creating a large number of assemblies (e.g. creating a lot of JavaScript *packages*) is not recommended since JS modules are file based while .NET modules are binary based.  This also means that with C#, you should feel free to organize related artifacts in namespaces however you see fit to reduce the friction of working with large blocks of imports like we see above.

For example, it can be advantageous to have several files in different directories all share the same namespace because they are often or always used together.  This means that a single `using My.Shared.Namespace` is all that's needed and thereby avoiding the wordiness of JS module imports.
:::
