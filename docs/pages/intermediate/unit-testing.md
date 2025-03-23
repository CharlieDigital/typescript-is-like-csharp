# Unit Testing

Both C# and TypeScript have mature and widely adopted unit testing libraries that help teams deliver robust software.

With TypeScript, teams will typically select **Jest** or **Vitest**.  There are multiple choices for unit testing in C# including **XUnit**, **NUnit**, **TUnit**, and Microsoft's first party **MSTest**.  In general, there isn't much variation between these, but there are some philosophical differences in how they handle setup and teardown with XUnit, for example, using the constructor and `IDisposable` as the setup and teardown interface.

However, the style and approach of writing unit tests tends to be different in C# and TypeScript with C# unit test frameworks generally adopting a class-based approach (with the exception of the **ScenarioTests** extension to NUnit) while both Jest and Vitest use a functional approach.

## Setup

For Node.js, we'll use [Vitest](https://vitest.dev/) and for C#, we'll give an up-and-coming library [TUnit](https://thomhurst.github.io/TUnit/) a try!

<CodeSplitter>
  <template #left>

```shell
# /src/typescript/vitest-example
npm init -y
tsc --init .
npm install -D vitest

# Mac, Linux
touch example.test.ts
touch model.ts

# Windows (PowerShell)
New-Item example.test.ts
New-Item model.ts
```

  </template>
  <template #right>

```shell
# /src/csharp/tunit-example
dotnet new classlib
dotnet add package TUnit # For unit testing
dotnet add package NSubstitute # For mocking

# Mac, Linux
mv Class1.cs Example.Test.cs
touch Model.cs

# Windows
ren Class1.cs Example.Test.cs
New-Item Model.cs

# At the root
dotnet sln add src/csharp/tunit-example
```

  </template>
</CodeSplitter>

For Node, we can set up scripts for different run modes for the test:

```json
// Modify package.json
{
  "scripts": {
    "test": "vitest run", // Run just once
    "test:watch": "vitest" // Run and watch for changes and re-run
  },
}
```

I find that testing tools on Node.js like Vitest to be quite nice in several ways:

1. They generally provide great options for output and visualization to the console whereas C# tools generally expect output to some *other* system for display (e.g. CI).
2. They provide more options for filtering tests like re-running only failed tests and so on.

## Basics

Let's create a simple model and a simple set of tests.

<CodeSplitter>
  <template #left>

```ts{23,29}
// 📄 model.ts
export class User {
  constructor(
    public readonly firstName: string,
    public readonly lastName: string,
    public readonly email: string
  ) {}

  get displayName() {
    return `${this.firstName} ${this.lastName}`;
  }

  get handle() {
    return `@${this.email.split("@")[0]}`;
  }
}

// 📄 example.test.ts
import { describe, test, expect } from "vitest";
import { User } from "./model";

describe("User creation", () => {
  test("user display name is formatted correctly", () => {
    const user = new User("Charles", "Chen", "chrlschn@example.org");

    expect(user.displayName).toBe("Charles Chen");
  });

  test("user handle is email with @", () => {
    const user = new User("Ada", "Lovelace", "alove@example.org");

    expect(user.displayName).toBe("Ada Lovelace");
    expect(user.handle).toBe("@alove");
  });
});


// npm run test 👈
//  ✓ example.test.ts (2 tests) 1ms
//    ✓ User creation > user display name is formatted correctly
//    ✓ User creation > user handle is email with @

//  Test Files  1 passed (1)
//       Tests  2 passed (2)
//    Start at  19:26:13
//    Duration  298ms (transform 72ms, setup 0ms, collect 49ms, tests 1ms, environment 0ms, prepare 67ms)
// ------------------------------
// npm run test:watch 👈 Test with watch mode

```

  </template>
  <template #right>

```csharp{14,15,21,22}
// 📄 Model.cs
public record User(
  string FirstName,
  string LastName,
  string Email
) {
  public string DisplayName => $"{FirstName} {LastName}";

  public string Handle => $"@{Email.Split('@')[0]}";
}

// 📄 Example.Test.cs
public class User_Creation {
  [Test]
  public async Task User_Display_Name_Is_Formatted_Correctly() {
    var user = new User("Charles", "Chen", "chrlschn@example.org");

    await Assert.That(user.DisplayName).IsEqualTo("Charles Chen");
  }

  [Test]
  public async Task User_Handle_Is_Email_With_At() {
    var user = new User("Ada", "Lovelace", "alove@example.org");

    await Assert.That(user.DisplayName).IsEqualTo("Ada Lovelace");
    await Assert.That(user.Handle).IsEqualTo("@alove");
  }
}

// dotnet test 👈
// Test summary: total: 2, failed: 0, succeeded: 2, skipped: 0, duration: 0.3s
// Build succeeded in 1.0s
// ------------------------------
// dotnet watch test 👈 Test with watch mode

```

  </template>
</CodeSplitter>

::: info
In C#, tests are always organized into classes and generally use attributes like `[Test]` to decorate the test cases whereas in TypeScript and JavaScript, the standard is to use a `describe()` compatible API.
:::

In Node, it is common practice to place tests along-side of your code whereas in C#, it is more common to place tests in a separate project.  This is because the typical build process for Node.js will strip/exclude files marked with `.test` or `.spec` so they are not included in the final output.

On the other hand, `.cs` files are built into a binary `.dll` so developers typically do not place them side-by-side.  However, this *can* be done (just not general practice) by simply excluding these files from the build process for the release configuration:

```xml
<!--
See: /src/csharp/ef-api/ef-api.csproj
-->
<Project>
  <!-- Remove .Test.cs files on release build -->
  <ItemGroup Condition="'$(Configuration)' == 'Release'">
    <Compile Remove="**\*.Tests.cs" />
  </ItemGroup>
</Project>
```

Now when the project is built for release, the test files will be excluded and tests can be placed alongside of the code.

One small downside to this is that because `.cs` files are compiled into a binary, this means that changes to only the test code will also require the code under test to be rebuilt as well whereas splitting out the test code into a separate project means that changes only to the test code do not require the code under test to be rebuilt.

## Mocking

Let's expand our model with a service and repository that interfaces with the database.

Unless we're writing integration tests, here we will want to replace the `UserRepository` with a mock so that we don't write to the database while we are testing our code.

Vitest has [mocking functionality](https://vitest.dev/guide/mocking.html) included so we'll use it.  For C#, we'll use [NSubstitute](https://nsubstitute.github.io/).

<CodeSplitter>
  <template #left>

```ts{22,30}
// 📄 model.ts
export class UserRepository {
  saveToDb(user: User) {
    // TODO: Actual database save.
    console.log("Saved user (from real repository");
  }
}

export class UserService {
  constructor(private readonly userRepository: UserRepository) {}

  saveUser(user: User) {
    // TODO: Do validation, prepare model, etc.
    this.userRepository.saveToDb(user);
  }
}

// 📄 example.test.ts
test("user saved to database", () => {
  const UserRepository = vi.fn();
  UserRepository.prototype.saveToDb = vi.fn(
    () => "Saved user (from mock repository)"
  );

  const repo = new UserRepository();
  const userService = new UserService(repo);
  const user = new User("Ada", "Lovelace", "alove@example.org");
  const msg = userService.saveUser(user);

  expect(msg).toBe("Saved user (from mock repository)");
});
```

  </template>
  <template #right>

```csharp{3,4,24,29}
// 📄 Model.ts
public class UserRepository {
  // 👇 Important: this needs to be virtual
  public virtual string SaveToDb(User user) {
    // TODO: Actual database save.
    return "Saved user (from real repository)";
  }
}

public class UserService(UserRepository userRepository) {
  public string SaveUser(User user) {
    // TODO: Do validation, prepare model, etc.
    return userRepository.SaveToDb(user);
  }
}

// 📄 Example.Test.ts
[Test]
public async Task User_Save_To_Database()
{
  var user = new User("Ada", "Lovelace", "alove@example.org");

  var mockRepo = Substitute.For<UserRepository>();
  mockRepo.SaveToDb(user).Returns("Saved user (from mock repository)");

  var userService = new UserService(mockRepo);
  var msg = userService.SaveUser(user);

  await Assert.That(msg).IsEqualTo("Saved user (from mock repository)");
}
```

  </template>
</CodeSplitter>

On the C# side, it is very important to note that C# unit testing requires that entities that require mocking are either implemented from interfaces OR have [`virtual` members](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual) which allow the member to be overridden in an inheriting class.

What mocking frameworks in C# do is to create a proxy that inherits from the class and replaces the the original call with a call to the proxy.

In JS, the function simply gets replaced.

## C# Unit Testing Tools

- Fluent assertions:
  - [Shouldly](https://docs.shouldly.org/)
  - [FluentAssertions](https://fluentassertions.com/introduction)
- Unit tests:
  - [XUnit](https://xunit.net/#documentation)
    - [ScenarioTests](https://github.com/koenbeuk/ScenarioTests)
  - [NUnit](https://docs.nunit.org/)
  - [TUnit](https://thomhurst.github.io/TUnit/)
- Mocking
  - [Moq](https://github.com/devlooped/moq)
  - [NSubstitute](https://nsubstitute.github.io/)
  - [FakeItEasy](https://fakeiteasy.github.io/docs/8.3.0/)
