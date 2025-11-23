# TypeScript is Like C#

See: https://typescript-is-like-csharp.chrlschn.dev

This repository publishes a set of docs that is aimed at helping teams that are evaluating options beyond TypeScript.  (Or alternatively, if you are a C# developer trying to map concepts to JavaScript/TypeScript).  It is meant to help educate and clear up some myths and misconceptions around C# and modern .NET.

Many teams may find themselves in this position after dealing with TypeScript issues and the reality that the underlying runtime is still JavaScript and all that implies.  It may be the case that your team ran into runtime issues with types, challenges at dev time as developers abuse TypeScript's dynamic and flexible type system, a lot of verbosity with authoring type schemas for validation, or gaps and issues with common Node.js ecosystem ORMs like Prisma.

Particularly on the backend, the dynamic type system of JS (and therefore, TS) can be a bane when it comes to ensuring data quality and system correctness.

I hope that these docs can help convince you that C# is a very realistic pathway if your team is already comfortable with TypeScript and frameworks like Nest.js.

Check out the published docs ***and please leave feedback***!

## Contributing

👋🏼 Hey there, interested in contributing?

Fork and make a PR!  Most of this repo is simply markdown so it's easy to contribute.

To run locally:

```shell
# Install dependencies
cd docs
yarn

# From the root
yarn --cwd docs docs:dev
```

## Code Examples

To test your code examples:

### TypeScript

1. Use [TypeScript Playground](https://www.typescriptlang.org/play/?#)
2. For more comprehensive examples, create a sample project in `src/typescript`
3. Use the `src/typescript/js-notebook.dib` for simple JavaScript examples

### C#

1. Use the C# notebook in `src/csharp/csharp-notebook.dib`
2. Create a single file app in `src/csharp/fileapps`
3. Create a full project and add it to the top-level solution.

## Authoring Tips

The code splitter is a custom Vue component.

To use the code splitter:

````html
<CodeSplitter>
  <template #left>

    ```ts
    // TS/JS code goes here
    ```

  </template>
  <template #right>

   ```cs
   // C# code goes here
   ```

  </template>
</CodeSplitter>
````

> 💡 ***Note*** the extra space after the opening `<template>` and before the closing `</template>`.  This is necessary for the VitePress pre-processor.

## Guidelines

- Focus on educating; you don't have to be able to fill in both sides (C# and JS); you can fill in one side and leave the other "WIP"
- Write clear, concise, and easy to follow examples
- Try to distill the examples down to the simplest use case that demonstrates the idea
- Use [VitePress markdown extensions](https://vitepress.dev/guide/markdown) where appropriate including code line highlighting
