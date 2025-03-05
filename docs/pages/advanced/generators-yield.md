# Generators and `yield`

Both C# and JavaScript/TypeScript use **generator functions** and the `yield` keyword to create iterators that allow functions to return values lazily without allocating memory for intermediate results. In JavaScript/TypeScript, a generator is defined using the `function*` syntax, and the `yield` keyword produces values one at a time. The function’s execution is paused and resumed using the `.next()` method of the iterator. This makes it useful for scenarios like lazy evaluation or asynchronous programming, where values are needed on demand, without the overhead of creating entire collections.

In C#, generators are implemented using `yield return` within methods that return an `IEnumerable<T>` or `IEnumerator<T>`. Like in JavaScript, C# generators allow you to produce values lazily, but they work within the `IEnumerable<T>` interface, where values are retrieved via `MoveNext()` and `Current`. A key benefit of both C# and JavaScript generators is that they are **allocationless**, meaning they do not require storing intermediate collections in memory, which makes them highly efficient when dealing with large datasets or infinite sequences.

## Example

<CodeSplitter>
  <template #left>

```ts{15,18,23}
// Target: ESNext, Module: CommonJS required to make this work
export {}

// An API call that produces a set of events
let getGcalEvents = async () => Promise.resolve([
  "gcal-1", "gcal-2"
]);

// An API call that produces a set of events
let getO365Events = async() => Promise.resolve([
  "o365-1", "o365-2"
]);

// Async generator function that virtualizes this set as an iterator
async function* getEvents() {
  let gcalEvents = await getGcalEvents();
  for (let evt of gcalEvents) {
    yield evt // 👈 Yield
  }

  let o365Events = await getO365Events();
  for (let evt of o365Events) {
    yield evt // 👈 Yield
  }
}

(async () => {
  // It looks like a single stream 👇
  for await (let evt of getEvents()) {
      console.log(evt)
  }
})()


```

  </template>
  <template #right>

```csharp{12,15,20}
// An API call that produces a set of events
var GetGcalEvents = async () => await Task.FromResult<string[]>([
  "gcal-1", "gcal-2"
]);

// An API call that produces a set of events
var GetO365Events = async () => await Task.FromResult<string[]>([
  "o365-1", "o365-2"
]);

// Async generator function that virtualizes this set as an iterator
async IAsyncEnumerable<string> GetEvents() {
  var gcalEvents = await GetGcalEvents();
  foreach (var evt in gcalEvents) {
    yield return evt; // 👈 Yield
  }

  var o365Events = await GetO365Events();
  foreach (var evt in o365Events) {
    yield return evt; // 👈 Yield
  }
}

// It looks like a single stream 👇
await foreach (var evt in GetEvents()) {
  Console.WriteLine(evt);
}
```

  </template>
</CodeSplitter>

Generators are very useful when working with multiple streams or even multiple sources of collections.  You can see that it "virtualizes" the underlying sets as a single, contiguous iterator and allows us to avoid an allocation (especially useful for large, streams of data).

::: tip
In C#, it pairs nicely with `System.Threading.Channels` to simplify combining multiple async streams concurrently *and in parallel*.
:::

## More Reading

- [JS generators](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Generator)
- [JS `AsyncGenerator`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/AsyncGenerator)
- [C# `yield return`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield)
- [C# `IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/archive/msdn-magazine/2019/november/csharp-iterating-with-async-enumerables-in-csharp-8)
