# TypeScript is Like C#

See: https://typescript-is-like-csharp.chrlschn.dev

This repository publishes a set of docs that is aimed at helping teams that are evaluating options beyond TypeScript.  (Or alternatively, if you are a C# developer trying to map concepts to JavaScript/TypeScript).  It is meant to help educate and clear up some myths and misconceptions around C# and modern .NET.

Many teams may find themselves in this position after dealing with TypeScript issues and the reality that the underlying runtime is still JavaScript and all that implies.  It may be the case that your team ran into runtime issues with types, challenges at dev time as developers abuse TypeScript's dynamic and flexible type system, a lot of verbosity with authoring type schemas for validation, or gaps and issues with common Node.js ecosystem ORMs like Prisma.

Particularly on the backend, the dynamic type system of JS (and therefore, TS) can be a bane when it comes to ensuring data quality and system correctness.

JavaScript's single-threaded nature also means that it can feel limiting for both runtime throughput and the possible tools developers can use to solve performance sensitive problems (e.g. processing a large CSV in parallel in a compute intensive scenario).

I hope that these docs can help convince you that C# is a very realistic pathway if your team is already comfortable with TypeScript and frameworks like Nest.js.

Check out the published docs ***and please leave feedback***!

## Contributing

👋🏼 Hey there, interested in contributing?

Fork and make a PR!  Most of this repo is simply markdown so it's easy to contribute.

To run locally:

```shell
# From the root
yarn --cwd docs docs:dev
```

## Guidelines

- No trashing either language; we're focused on educating
- Write clear, concise, and easy to follow examples
- Try to distill the examples down to the simplest use case that demonstrates the idea
- Write both TS and C# except for cases where it's focused on a C# specific language feature
- Use [VitePress markdown extensions](https://vitepress.dev/guide/markdown) where appropriate including code line highlighting
