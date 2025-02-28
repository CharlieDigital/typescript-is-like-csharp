<script setup>
import CodeSplitter from '../../components/CodeSplitter.vue'
</script>

# Async/Await

In C#, the `Task` class is the equivalent of `Promise`.  However, it is important to make the distinction here that .NET's runtime is concurrent + parallel (because it is multi-threaded) which means that given a set of *futures*, they may be executed on *different threads* whereas in JS, they will always execute on a single thread.

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
