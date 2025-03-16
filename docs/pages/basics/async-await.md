# Async/Await

Both C# and TypeScript/JavaScript use **async/await** to handle asynchronous operations, providing a cleaner, more readable alternative to traditional callbacks or promises. In both languages, the `async` keyword marks a function as asynchronous, and the `await` keyword pauses the function’s execution until a `Promise` (in JavaScript/TypeScript) or a `Task` (in C#) is resolved. In JavaScript and TypeScript, `Promise` objects represent the eventual completion (or failure) of an asynchronous operation, while in C#, `Task` represents an ongoing operation that will complete in the future. This makes handling asynchronous operations in both languages straightforward, as both use these constructs to write asynchronous code in a synchronous-looking style.

However, C# takes it a step further by supporting **multithreading** and **parallelism** in addition to simple asynchronous tasks. While JavaScript is single-threaded and typically runs asynchronous tasks in a non-blocking event loop, C# can leverage the **`ThreadPool`** and **`Parallel`** libraries to run multiple tasks in parallel, utilizing multiple CPU cores. This is particularly useful for CPU-bound tasks where true parallel execution is required, such as performing calculations or processing large datasets. In contrast, TypeScript and JavaScript’s asynchronous model (through `Promises`) is only suited for I/O-bound tasks, like handling HTTP requests or reading files, and does not inherently perform operations concurrently across multiple threads.

## Basics

<CodeSplitter>
  <template #left>

```ts
async function fetchProfiles(): Promise<Profile[]> {
  return await service.getProfiles();
}

let results = await fetchProfiles();
```

  </template>
  <template #right>

```csharp
public async Task<Profile[]> FetchProfilesAsync() {
  return await service.GetProfilesAsync();
}

var results = await FetchProfilesAsync();
```

  </template>
</CodeSplitter>

::: tip
In C#, the `-Async` is conventional nomenclature; you do ***not*** have to use it.  You can name your method `FetchProfiles()` and it will work just as well.  Naming it `FetchProfilesAsync` is just the idiomatic way of naming it to indicate that it returns a `Task`.
:::

## Concurrency

<CodeSplitter>
  <template #left>

```ts
async function fetchUsers() : Promise<User[]>{ }
async function fetchChats() : Promise<Chat[]>{ }

await Promise.all([
  fetchUsers(),
  fetchChats()
])

// With destructured results
let [users, chats] = await Promise.all([
  fetchUsers(),
  fetchChat()
])
```

  </template>
  <template #right>

```csharp
async Task<User[]> FetchUsersAsync() { }
async Task<Chat[]> FetchChatsAsync() { }

await Task.WhenAll(
  FetchUsersAsync(),
  FetchChatsAsync()
)

// With destructured results (see note below)
var (users, chats) = await (
  FetchUsersAsync(),
  FetchChatsAsync()
)
```

  </template>
</CodeSplitter>

In C#, we need to add a static **extension method** to **`ValueTuple`** (we'll cover tuples and extension methods later) to enable this behavior.  Here is a sample implementation borrowed from [here](https://github.com/meziantou/Meziantou.Framework/blob/b60a0accfb4fa9f58b3cc7ce05ac59a1e7f7a809/src/Meziantou.Framework/TaskEx.WhenAll.cs):

```cs
public static class TaskEx {
  public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(
    this ValueTuple<Task<T1>, Task<T2>> tasks
  ) {
    return WhenAll(tasks.Item1, tasks.Item2).GetAwaiter();
  }

  public static async Task<(T0, T1)> WhenAll<T0, T1>(
    Task<T0> task0, Task<T1> task1
  ) {
    await Task.WhenAll(task0, task1).ConfigureAwait(false);
    return (task0.Result, task1.Result);
  }
}
```

This extension method allows us to simplify the code and extract the results.

::: warning
There is one very important distinction between `Task` and `Promise`: `Task` can be both concurrent ***and*** parallel while `Promise` is only concurrent.  `Task` can run in different threads on .NET's thread pool, which is not the case for `Promise` as it is single threaded.  So some care needs to be taken when mutating state like using `Interlocked` or structures like `ConcurrentDictionary` and `ConcurrentBag`.
:::

::: tip Read more on C# `async/await`
Dive deeper into the details of `async/await` in C# and best practices [in David Fowler's writeup](https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md).
:::

## Concurrency and Parallelism

A simple analogy for concurrency is a waiter in a restaurant.

- The waiter is responsible for taking orders and delivering them to the kitchen (`async placeOrder() { }`).
- While the waiter is waiting for the dish to be prepared, the waiter can go back to the dining area to take more orders, deliver completed orders, clean tables, and help final prep; the waiter can ***concurrently*** work on various tasks while waiting for an order to be ready.
- Then once an order is ready, the waiter goes back to the kitchen to retrieve and deliver the order to the diner (`const order = await placeOder()`)

This is exactly how the single-threaded event loop of Node.js works and also how concurrency works conceptually in .NET.

So what's different with .NET?  Because the runtime is multi-threaded, there is a *thread pool* and *task scheduler* that schedules work on multiple threads.  In our analogy, it's like having multiple waiters.  With multiple waiters, there are of course considerations like coordinating which waiter services which tables.  Likewise, in a multi-threaded environment, it is sometimes necessary to understand how to manage *synchronization* for workloads spread across different threads.

Don't let this scare you off!  Most of the time, this is transparent because the framework provide primitives like `ConcurrentBag` and `ConcurrentDictionary` as well as `Interlocked`.  The DI container is also designed to make it easier to manage object dependencies with different lifecycles (singleton, per-request, transient).  Request scoped variables access is single-threaded unless the code path explicitly starts multiple threads (e.g. via Task Parallel Library).
