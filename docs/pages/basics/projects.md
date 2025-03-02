# Packages and Projects

C# projects are analogous to Node packages.  When you add packages from [Nuget](https://www.nuget.org/) (.NET's package registry), they get registered in the `.csproj` file.  Likewise, when packages from [NPM](https://www.npmjs.com/) are added to a Node.js project, it is registered in `package.json`

To execute commands in a `.csproj`, we need to use declarative XML like this:

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
