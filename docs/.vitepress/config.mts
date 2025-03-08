import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "TypeScript is Like C#",
  description: "Outgrowing Node.js? If you already know TypeScript, then learning C# is easy!  This guide walks you through the similarities (and differences) between TypeScript and C#",
  sitemap: {
    hostname: 'https://typescript-is-like-csharp.chrlschn.dev'
  },
  head: [
    ['link', { rel: 'icon', href: '/csharp-logo-32.png' }],
    ['meta', { property: 'og:image', content: 'https://typescript-is-like-csharp.chrlschn.dev/typescript-csharp.png' }],
    ['meta', { property: 'twitter:image', content: 'https://typescript-is-like-csharp.chrlschn.dev/typescript-csharp.png' }],
    ['script', {
      async: '',
      src: 'https://www.googletagmanager.com/gtag/js?id=G-QLSJ4QC7YZ'
    }],
    ['script', {}, `
      window.dataLayer = window.dataLayer || [];
      function gtag(){dataLayer.push(arguments);}
      gtag('js', new Date());
      gtag('config', 'G-QLSJ4QC7YZ');
    `],
  ],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    logo: '/csharp-logo.png',

    nav: [
      { text: 'Home', link: '/' },
      { text: 'Intro', link: '/pages/intro-and-motivation' },
      { text: 'About', link: 'https://www.linkedin.com/in/charlescchen/' },
    ],

    editLink: {
      pattern: 'https://github.com/CharlieDigital/typescript-is-like-csharp/blob/main/docs/:path'
    },

    sidebar: [
      {
        text: 'Guide',
        items: [
          { text: 'Intro and Motivation', link: '/pages/intro-and-motivation' },
          { text: 'Getting Started', link: '/pages/getting-started' },
          {
            text: 'Basics',
            collapsed: false,
            items: [
              { text: 'Variables', link: '/pages/basics/variables' },
              { text: 'Types', link: '/pages/basics/types' },
              { text: 'Nulls', link: '/pages/basics/nulls' },
              { text: 'Strings', link: '/pages/basics/strings' },
              { text: 'Collections', link: '/pages/basics/collections' },
              { text: 'Error Handling', link: '/pages/basics/error-handling' },
              { text: 'Conditionals', link: '/pages/basics/conditionals' },
              { text: 'Iteration', link: '/pages/basics/iteration' },
              { text: 'Comments', link: '/pages/basics/comments' },
              { text: 'Functions', link: '/pages/basics/functions' },
              { text: 'Classes and Types', link: '/pages/basics/classes' },
              { text: 'Generics', link: '/pages/basics/generics' },
              { text: 'Async/Await', link: '/pages/basics/async-await' },
              { text: 'Packages vs Projects', link: '/pages/basics/projects' },
              { text: 'import vs using', link: '/pages/basics/import-vs-using' },
              { text: 'CLI and Tooling', link: '/pages/basics/cli-tooling' },
            ]
          },
          {
            text: 'Intermediate',
            collapsed: true,
            items: [
              { text: 'LINQ', link: '/pages/intermediate/linq' },
              { text: 'Tuples', link: '/pages/intermediate/tuples' },
              // { text: 'Records', link: '/pages/intermediate/records' },
              { text: 'Extension Methods', link: '/pages/intermediate/extension-methods' },
              { text: 'Iterators and Enumerables', link: '/pages/intermediate/iterators-enumerables' },
              { text: 'Unit Testing', link: '/pages/intermediate/unit-testing' },
              { text: 'Express vs Minimal API', link: '/pages/intermediate/express-vs-minimal-api' },
              { text: 'Nest.js vs Controller API', link: '/pages/intermediate/nest-vs-controller-api' },
              { text: 'Decorators vs Attributes', link: '/pages/intermediate/decorators-vs-attributes' },
              { text: 'Dependency Injection', link: '/pages/intermediate/dependency-injection' },
              { text: 'Entity Framework Core (ORM)', link: '/pages/intermediate/ef-core' },
            ]
          },
          {
            text: 'Advanced',
            collapsed: true,
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
            collapsed: true,
            items: [
              { text: 'Switch Expression', link: '/pages/bonus/switch-expression' },
              { text: 'Partial Classes', link: '/pages/bonus/partial-classes' },
              { text: 'Global Using', link: '/pages/bonus/global-usings' }
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
