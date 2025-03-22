# Set Up Formatters

C# has a number of options for auto-formatting code using either the [`dotnet format`](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format) CLI command or using a third party tool like the Prettier inspired [CSharpier](https://csharpier.com/).  CSharpier is more opinionated and has less configuration overhead.

For TypeScript and JavaScript, Prettier is the standard formatter that most teams use and it is typically pre-configure with many templates.  In this example, we'll examine how to configure it from scratch in an empty project.

::: tip If you prefer your C# to be formatted like your TS
If you have a preference for this, then use `dotnet format` since it is possible to use [the `.editorconfig` file](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options) to control formatting.  CSharpier's default formatting is **Allman style** while TS is typically **K&R style**.  The repo has examples of both setups.
:::

## Basic Setup

<CodeSplitter>
  <template #left>

```shell
# /src/typescript/prettier-example
tsc --init .
npm init -y
npm install --save-dev --save-exact prettier

touch .prettierrc # Create config file (empty)
touch index.ts # Create simple sample file (with placeholder code)

# Install: https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode
```

  </template>
  <template #right>

```shell
# /src/csharp/csharpier-example
dotnet new webapi
dotnet new tool-manifest
dotnet tool install csharpier

dotnet csharpier .  # Manually run

# Install: https://marketplace.visualstudio.com/items?itemName=csharpier.csharpier-vscode
```

  </template>
</CodeSplitter>

## Configure Auto Formatting

<CodeSplitter>
  <template #left>

```json
// .vscode/settings.json
"[typescript]": {
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode"
},
```

  </template>
  <template #right>

```json
// .vscode/settings.json
"[csharp]": {
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "csharpier.csharpier-vscode"
},
```

  </template>
</CodeSplitter>

Here is some sample output:

<CodeSplitter>
  <template #left>

```ts
// Prettier (2-space, K&R)
class Person {
  constructor(
    public firstName: string,
    public lastName: string,
  ) {}

  get displayName(): string {
    return `${this.firstName} ${this.displayName}`;
  }

  contact() {
    console.log("Contacting...");
  }
}
```

  </template>
  <template #right>

```csharp
// CSharpier (4-space, Allman)
class Person
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string DisplayName => $"{FirstName} {LastName}";

    public void Contact()
    {
        Console.WriteLine("Contacting...");
    }
}
```

  </template>
</CodeSplitter>

## Configuring `.editorconfig`

To format C# like TypeScript, you'll need to use `dotnet format` and the `.editorconfig` file.

::: warning
Unfortunately, there is no format-on-save for `dotnet format` so typically, this setup would be enforced on a pre-commit hook and can be used for manual formatting. It is also noticeably slower than CSharpier and Prettier.  Even though CSharpier does not format the same as Prettier, it might be better so that it is easier to discern between backend and frontend code.
:::

```ini
# See: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/csharp-formatting-options

# Remove the line below if you want to inherit .editorconfig settings from higher directories
root = true

# C# files
[*.cs]

#### Core EditorConfig Options ####

# Indentation and spacing
indent_size = 2
indent_style = space
tab_width = 2

#### C# Coding Conventions ####

# var preferences
csharp_style_var_elsewhere = true
csharp_style_var_for_built_in_types = true
csharp_style_var_when_type_is_apparent = true

#### C# Formatting Rules ####

# New line preferences
csharp_new_line_before_catch = false
csharp_new_line_before_else = false
csharp_new_line_before_finally = false
csharp_new_line_before_members_in_anonymous_types = true
csharp_new_line_before_members_in_object_initializers = true
csharp_new_line_before_open_brace = none
csharp_new_line_between_query_expression_clauses = true
```

See [the official guide](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options) for more examples or try [this generic one from GitHub](https://github.com/RehanSaeed/EditorConfig).
