# Partial Classes and Functions

**Partial classes** and **partial methods** in C# offer a way to split the definition of a class or method across multiple files, making it easier to organize and manage large codebases. A **partial class** allows a single class to be divided into multiple parts, potentially in different files, which can help keep large classes manageable and improve code readability. This is especially useful in large projects where multiple developers work on different aspects of a class, such as when implementing features or maintaining legacy code. The compiler merges all the partial class definitions at compile time, so they behave as a single class in the final application.

**Partial methods** work alongside partial classes and allow method declarations to be split across files, with the actual implementation optional. A partial method is defined with a `partial` keyword, and if no implementation is provided, it is ignored by the compiler, which is useful for **code generation scenarios**. For example, tools that generate code can declare partial methods, leaving the implementation to the developer or automatically inserting generated code. This enables custom extensions without modifying generated code directly, supporting scenarios where automatic code generation or scaffolding is used (e.g., in frameworks, ORM tools, or designers).

This combination of partial classes and methods provides a flexible mechanism for breaking up large codebases, supporting better organization, and enabling scenarios where code generation and manual code coexist seamlessly.

## Managing Large Classes

<CodeSplitter>
  <template #left>

```ts
type User = {}

// 📃 user-repository.ts
// 👇 Assume that this is a large repository with many functions
class UserRepository {

}

// Interface to support .prototype
interface UserRepository {
  readAll() : User[];
  readSome() : User[];
  create(user: User): void;
  update(user: User): void;
  delete(id: string): void;
}

// 📃 user-repository.read.ts to break out all reads
UserRepository.prototype.readAll = function() {
  return []
}

UserRepository.prototype.readSome = function(filter: string) {
  return []
}

// 📃 user-repository.write.ts to break out all writes
UserRepository.prototype.create = function(user: User) {

}

UserRepository.prototype.update = function(user: User) { }

// 📃 user-repository.delete.ts to break out all deletions
UserRepository.prototype.delete = function(id: string) { }
```

  </template>
  <template #right>

```csharp
class User { }

// 📃 UserRepository.cs
// 👇 Assume that this is a large repository with many functions
partial class UserRepository {
  // 👇 Partial method; behaves like a "contract"
  partial void Delete(string id);
}

// 📃 UserRepository.Read.cs to break out all reads
partial class UserRepository {
  User[] ReadAll() => Array.Empty<User>();

  User[] ReadSome(string filter) => Array.Empty<User>();
}

// 📃 UserRepository.Write.cs to break out all writes
partial class UserRepository {
  void Create(User user) { }

  void Update(User user) { }
}

// 📃 UserRepository.Delete.cs to break out all deletions
partial class UserRepository {
  partial void Delete(string id) {
    // Actual implementation here
  }
}
```

  </template>
</CodeSplitter>

The C# version is less verbose and requires less effort to maintain (no need to maintain the extraneous interface).

But on top of that, it allows the use of [partial *members*](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/partial-member) including properties.  The main use case for this is dev-time source generation where it is then possible to have Roslyn source generators dynamically create partial implementations at dev-time.

## Source Generation

Here is an example from [Classes and Types](../basics/classes.md#type-unions) using the `OneOf` library with a source generator:

```csharp
enum TrainType { Bullet, Normal }

record Car(int numSeats);
record Scooter(bool electric);
record Train(TrainType type);

namespace Transit { // OneOf requires a namespace
  // 👇 Partial class declaration where the source generator will create impl
  [GenerateOneOf]
  partial class TransitOption : OneOfBase<Car, Scooter, Train> { };
}
```

This open source project [dn7-source-generators](https://github.com/CharlieDigital/dn7-source-generators/) shows how we can use partial classes to "augment" the generated classes:

```csharp
// Generated partial class
public partial class OrderRepository : RepositoryBase<Order> {
  public void Test() {
    Console.WriteLine("runtime.models");
  }
}

// User implemented partial class
public partial class OrderRepository {
  // This method doesn't exist on the contract `RepositoryBase<T>` or the
  // generated code; we can extend our generated source with more methods
  public async Task UpdateIfNotShipped(Order entity) {
    await Task.CompletedTask;
    Console.WriteLine("Order → UpdateIfNotShipped");
  }
}
