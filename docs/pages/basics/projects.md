# Packages and Projects

C# projects are analogous to Node packages.  When you add packages from [Nuget](https://www.nuget.org/) (.NET's package registry), they get registered in the `.csproj` file.  Likewise, when packages from [NPM](https://www.npmjs.com/) are added to a Node.js project, it is registered in `package.json`

## Adding Projects and References

Both support the concept of projects. In .NET, these are sometimes called "libraries" which are compiled into binary assemblies (unlike TypeScript which retains text as it is compiled to JavaScript).

Typically, C# projects will have a `.sln` file which you can think of as being equivalent to a workspace config file for Node.js projects.  When you add additional projects, these need to be registered in the `.sln` file.  The `.sln` is created automatically and typically I would avoid editing manually.  There's not much setup involved on the C# side

For Node, different project managers have different workspace configurations.  Here, [we'll look at NPM workspaces](https://docs.npmjs.com/cli/v7/using-npm/workspaces).  There's a bit more setup required and it's different depending on your preferred package manager.

<CodeSplitter>
  <template #left>

```ts
🚧 WIP
```

  </template>
  <template #right>

```shell
# C# setup for multi-project workspace
# root
#  └ project_1
#    └ project_1.csproj
#  └ project_2
#    └ project_2.csproj
# root.sln

# At /root
# Add a new project
mkdir project_3
cd project_3
dotnet new classlib    # Create a class library (like a package; no entry point)

cd ../ # Back to root
dotnet sln add project_3 # Path to the new project

# Add a reference from project_1 to project_3
cd project_1
dotnet add reference ../project_3
```

  </template>
</CodeSplitter>

## Executing Commands

.NET's `.csproj` doesn't innately support executing named commands.  Instead, it can execute pre- and post-build commands.  To execute commands, write the batch files or shell scripts directly instead.

To execute pre- or post- commands in a `.csproj`, we need to use declarative XML like this:

```xml{10-14,28-34}
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.ApiDescription.Server" Version="9.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

  <!--
    This property group contains the directives for generating the
    OpenAPI specification.
  -->
  <PropertyGroup>
    <!-- The output directory (placed one level up in this case) -->
    <OpenApiDocumentsDirectory>../openapi-spec</OpenApiDocumentsDirectory>
    <!-- The file name -->
    <OpenApiGenerateDocumentsOptions>--file-name api-spec</OpenApiGenerateDocumentsOptions>
  </PropertyGroup>

  <!-- This section is a set of post-build commands -->
  <Target Name="GenerateSpec" AfterTargets="Build" Condition="$(Configuration)=='Gen' Or $(GEN)=='true'">
    <Message Text="Generating OpenAPI schema file." Importance="high" />

    <!-- Generate TS bindings for the web app -->
    <Exec Command="yarn gen" WorkingDirectory="../vue-spa" />
  </Target>
</Project>
```

Like packages in a workspace in a typical TypeScript project, .NET projects can reference other local projects as well.  We'll look at this in the next section.

You can learn more about the `.csproj` format in [the documentation](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-reference?view=vs-2022).
