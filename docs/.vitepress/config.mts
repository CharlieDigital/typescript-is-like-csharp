import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "TypeScript is Like C#",
  description: "TypeScript is like C# (it really is!)",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: 'Home', link: '/' },
      { text: 'Intro', link: '/intro-and-motivaation' }
    ],

    editLink: {
      pattern: 'https://github.com/CharlieDigital/typescript-is-like-csharp/docs/:path'
    },

    sidebar: [
      {
        text: 'Examples',
        items: [
          { text: 'Intro and Motivation', link: '/intro-and-motivation' },
          {
            text: 'Basics',
            collapsed: false,
            items: [
              { text: 'Variables', link: '/basics-variables' },
              { text: 'Nulls', link: '/basics-nulls' },
              { text: 'Strings', link: '/basics-strings' },
              { text: 'Collections', link: '/basics-collections' },
              { text: 'Error Handling', link: '/basics-error-handling' },
              { text: 'Conditionals', link: '/basics-conditionals' },
              { text: 'Iteration', link: '/basics-iteration' },
              { text: 'Generics', link: '/basics-generics' },
              { text: 'Functions', link: '/basics-functions' },
              { text: 'Classes and Types', link: '/basics-classes' },
              { text: 'Async/Await', link: '/basics-async-await' },
              { text: 'Packages vs Projects', link: '/basics-projects' },
            ]
          },
          {
            text: 'Intermediate',
            collapsed: false,
            items: [
              { text: 'LINQ', link: '/intermediate-linq' },
              { text: 'Tuples', link: '/intermediate-tuples' },
              { text: 'Records', link: '/intermediate-records' },
              { text: 'Iterators and Enumerables', link: '/intermediate-iterators-enumerables' },
              { text: 'Express vs Minimal API', link: '/intermediate-express-vs-minimal-api' },
              { text: 'Nest.js vs Controller API', link: '/intermediate-nest-vs-controller-api' },
              { text: 'Dependency Injection', link: '/intermediate-dependency-injection' },
              { text: 'Entity Framework Core (ORM)', link: '/intermediate-ef-core' },
            ]
          },
          {
            text: 'Advanced',
            collapsed: false,
            items: [
              { text: 'Generators and Yield', link: '/advanced-generators-yield' },
              { text: 'dynamic (ExpandoObject)', link: '/advanced-dynamic' },
              { text: 'JSON Serialization', link: '/advanced-json-serialization' },
              { text: 'Reflection', link: '/advanced-reflection' },
              { text: 'Channels', link: '/advanced-channels' },
              { text: 'Source Generation (Roslyn)', link: '/advanced-source-generation-roslyn' },
            ]
          },
          {
            text: 'Ergonomics',
            collapsed: false,
            items: [
              { text: 'Switch Expression', link: '/niceties-switch-expression' },
              { text: 'Partial Classes', link: '/niceties-partial-classes' }
            ]
          },
        ]
      }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/vuejs/vitepress' }
    ]
  }
})
