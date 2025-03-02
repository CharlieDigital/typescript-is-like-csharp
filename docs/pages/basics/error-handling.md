# Error Handling

Error handling in both languages is mostly congruent (as opposed to Go, for example).  Both follow the `try-catch-finally` exception handling construct.

Error handling in C# is centered around **exceptions**, which are objects representing runtime errors. Unlike JavaScript and TypeScript, where errors can be any value (including simple strings), C# enforces structured exception handling using the `try`, `catch`, `finally`, and `throw` keywords. Exceptions in C# derive from `System.Exception`, allowing for a robust type hierarchy where specific exceptions (e.g., `NullReferenceException`, `ArgumentException`) provide more context about failures. Additionally, C# supports `checked` and `unchecked` contexts for numeric overflow handling, and it enforces compile-time checking for unhandled exceptions, making error management more predictable than in JavaScript.

In contrast, JavaScript and TypeScript use a more flexible error-handling approach. While they support `try...catch...finally`, errors are not required to follow a strict class-based structure—any value, even a string, can be thrown. TypeScript improves error handling slightly by enabling type annotations, but it does not enforce exception types like C#. Additionally, unhandled promise rejections in JavaScript (from asynchronous operations) can silently fail if not explicitly caught, whereas C#’s `Task` and `async` methods throw exceptions that must be handled explicitly, reducing the risk of silent failures.

## Throwing Exceptions

<CodeSplitter>
  <template #left>

```ts
throw new Error("Oops!");
```

  </template>
  <template #right>

```csharp
throw new Exception("Oops!");
```

  </template>
</CodeSplitter>

## Try-Catch-Finally

<CodeSplitter>
  <template #left>

```ts
try {
  // Work here
} catch {
  // Handle error here
}

try {
  // Work here
} catch (err) {
  // Handle error here
} finally {
  // Always executed
}
```

  </template>
  <template #right>

```csharp
try {
  // Work here
} catch {
  // Handle error here
}

try {
  // Work here
} catch (Exception ex) {
  // Handle error here
} finally {
  // Always executed
}
```

  </template>
</CodeSplitter>

## Exception Types

<CodeSplitter>
  <template #left>

```ts
class NotFoundError extends Error {
  constructor(message) {
    super(message)
  }
}
```

  </template>
  <template #right>

```csharp
class NotFoundException : Exception {
  public NotFoundException(string message)
    : base(message) { }
}

// Using a primary constructor (see later docs)
class NotFoundException(
  string message
) : Exception(message) { }
```

  </template>
</CodeSplitter>

Now we can filter on the type of exception.  The mechanism is cleaner in C#.

<CodeSplitter>
  <template #left>

```ts
try {
  // Work here
} catch (err) {
  if (err instanceof NotFoundError) {
    // Handle NotFoundError
  } else {
    // Handle all other errors
  }
} finally {
  // Always executed
}
```

  </template>
  <template #right>

```csharp
try {
  // Work here
} catch (NotFoundException) {
  // Handle NotFoundException
} catch (Exception) {
  // Handle all generic exceptions
} finally {
  // Always executed
}
```

  </template>
</CodeSplitter>

## Best Practices

### Rethrowing

<CodeSplitter>
  <template #left>

```ts
try {
  // Work here
} catch (err) {
  // Handle then rethrow
  throw err;
} finally {
  // Always executed
}
```

  </template>
  <template #right>

```csharp
try {
  // Work here
} catch (Exception) {
  // 👇 NOTE that this DOES NOT use `throw ex;`
  throw;
} finally {
  // Always executed
}
```

  </template>
</CodeSplitter>

::: warning
In C#, `catch(Exception ex) { throw; }` is not the same as `catch(Exception ex) { throw ex; }`.  In the former, the stack trace is maintained.  In the latter, the stack trace will be reset.  Prefer the former versus the latter!
:::
