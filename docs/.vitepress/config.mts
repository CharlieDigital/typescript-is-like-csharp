import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "TypeScript is Like C#",
  description: "TypeScript is like C# (it really is!)",
  head: [['link', { rel: 'icon', href: '/csharp-logo-32.png' }]],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: 'Home', link: '/' },
      { text: 'Intro', link: '/pages/intro-and-motivation' }
    ],

    editLink: {
      pattern: 'https://github.com/CharlieDigital/typescript-is-like-csharp/docs/:path'
    },

    sidebar: [
      {
        text: 'Examples',
        items: [
          { text: 'Intro and Motivation', link: '/pages/intro-and-motivation' },
          {
            text: 'Basics',
            collapsed: false,
            items: [
              { text: 'Variables', link: '/pages/basics/variables' },
              { text: 'Nulls', link: '/pages/basics/nulls' },
              { text: 'Strings', link: '/pages/basics/strings' },
              { text: 'Collections', link: '/pages/basics/collections' },
              { text: 'Error Handling', link: '/pages/basics/error-handling' },
              { text: 'Conditionals', link: '/pages/basics/conditionals' },
              { text: 'Iteration', link: '/pages/basics/iteration' },
              { text: 'Generics', link: '/pages/basics/generics' },
              { text: 'Functions', link: '/pages/basics/functions' },
              { text: 'Classes and Types', link: '/pages/basics/classes' },
              { text: 'Async/Await', link: '/pages/basics/async-await' },
              { text: 'Packages vs Projects', link: '/pages/basics/projects' },
            ]
          },
          {
            text: 'Intermediate',
            collapsed: false,
            items: [
              { text: 'LINQ', link: '/pages/intermediate/linq' },
              { text: 'Tuples', link: '/pages/intermediate/tuples' },
              { text: 'Records', link: '/pages/intermediate/records' },
              { text: 'Iterators and Enumerables', link: '/pages/intermediate/iterators-enumerables' },
              { text: 'Express vs Minimal API', link: '/pages/intermediate/express-vs-minimal-api' },
              { text: 'Nest.js vs Controller API', link: '/pages/intermediate/nest-vs-controller-api' },
              { text: 'Dependency Injection', link: '/pages/intermediate/dependency-injection' },
              { text: 'Entity Framework Core (ORM)', link: '/pages/intermediate/ef-core' },
            ]
          },
          {
            text: 'Advanced',
            collapsed: false,
            items: [
              { text: 'Generators and Yield', link: '/pages/advanced/generators-yield' },
              { text: 'dynamic (ExpandoObject)', link: '/pages/advanced/dynamic' },
              { text: 'JSON Serialization', link: '/pages/advanced/json-serialization' },
              { text: 'Reflection', link: '/pages/advanced/reflection' },
              { text: 'Channels', link: '/pages/advanced/channels' },
              { text: 'Source Generation (Roslyn)', link: '/pages/advanced/source-generation-roslyn' },
            ]
          },
          {
            text: 'Ergonomics',
            collapsed: false,
            items: [
              { text: 'Switch Expression', link: '/pages/bonus/switch-expression' },
              { text: 'Partial Classes', link: '/pages/bonus/partial-classes' }
            ]
          },
        ]
      }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/CharlieDigital/typescript-is-like-csharp' }
    ]
  }
})
