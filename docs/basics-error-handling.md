<script setup>
import CodeSplitter from './components/CodeSplitter.vue'
</script>

# Error Handling

Error handling in both languages is identical and congruent (as opposed to Go, for example).  Both follow the `try-catch-finally` exception handling construct.

## Basics

### Throwing Exceptions

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

### Try-Catch-Finally

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
  // Handle...
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
