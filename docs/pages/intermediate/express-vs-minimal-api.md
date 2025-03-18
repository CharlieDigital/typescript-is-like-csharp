# Express.js vs Minimal API

Both **Express.js** and **.NET Minimal Web APIs** provide lightweight ways to build web applications, but they differ in performance and built-in features. **Express.js** is a flexible, unopinionated web framework for Node.js, known for its simplicity and middleware-driven architecture. However, since Express relies heavily on JavaScript’s single-threaded event loop, CPU-intensive tasks can become bottlenecks, often requiring worker threads or external services to scale efficiently. Additionally, production-level features like request validation, authentication, and security protections typically require third-party middleware.

**.NET Minimal Web APIs**, introduced in .NET 6, offer a streamlined way to build APIs with high performance and **built-in production-ready features** (no hunting for NPM packages!). Unlike Express, .NET Minimal APIs leverage the highly optimized ASP.NET Core pipeline, benefiting from asynchronous request handling, automatic dependency injection, and built-in middleware for logging, authentication, and rate limiting—many of which only need to be progressively enabled and configured rather than installed separately.

## Setting Up

We'll follow [this guide](https://blog.logrocket.com/how-to-set-up-node-typescript-express/) to set up Express with TypeScript

<CodeSplitter>
  <template #left>

```shell
# macOS Express.js TypeScript setup
npm init -y           # Init package.json
npm i express dotenv  # Install express and dotenv
echo PORT=3000 .env   # Create the .env file

# Setup TypeScript
npm i -D typescript @types/express @types/node
npx tsc --init # Initialize TypeScript

# Edit tsconfig.json compilerOptions.outDir = "./dist"

# Create the entry point:
echo "import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';

dotenv.config();

const app: Express = express();
const port = process.env.PORT || 3000;

app.get('/', (req: Request, res: Response) => {
  res.send('Express + TypeScript Server');
});

app.listen(port, () => {
  console.log('Server is running at http://localhost:' + port);
});" > index.ts

# Add packages to support hot reload
npm i -D nodemon ts-node concurrently

# Update package.json
# "scripts": {
#  "build": "npx tsc",
#  "start": "node dist/index.js",
#  "dev": "nodemon src/index.ts"
# }

npm run dev # ✅ Server ready!
```

  </template>
  <template #right>

```shell
# macOs .NET Minimal API setup

# Scaffold the API
dotnet new webapi

# Run and watch for file changes
dotnet watch --non-interactive # ✅ Server ready!
```

  </template>
</CodeSplitter>

::: tip Source code
💡 The source code for this walkthrough is available in GitHub [for .NET](https://github.com/CharlieDigital/typescript-is-like-csharp/tree/main/src/csharp/webapi-minimal) and [for Express.js](https://github.com/CharlieDigital/typescript-is-like-csharp/tree/main/src/typescript/express-app).
:::

## Application

Let's take a look at the application that's created:

<CodeSplitter>
  <template #left>

```ts
import express, { Express, Request, Response } from 'express';
import dotenv from 'dotenv';

dotenv.config();

const app: Express = express();
const port = process.env.PORT || 3000;

app.get('/', (req: Request, res: Response) => {
  res.send('Express + TypeScript Server');
});

app.listen(port, () => {
  console.log('Server is running at http://localhost:' + port);
});

```

  </template>
  <template #right>

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[] {
  "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () => {
  var forecast =  Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast (
      DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      Random.Shared.Next(-20, 55),
      summaries[Random.Shared.Next(summaries.Length)]
    ))
    .ToArray();
  return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

```

  </template>
</CodeSplitter>

Of note is that the .NET minimal web API also includes an OpenAPI schema as well as HTTPS redirection set up without any effort.  .NET's minimal web API starts with minimal scaffolding and features can be progressively turned on and there is no need to pick and choose packages to get a production ready web server.

## Minimal

Let's trim it down and compare like to like:

<CodeSplitter>
  <template #left>

```ts
import express, { Express, Request, Response } from 'express';

const app: Express = express();

app.get('/', (req: Request, res: Response) => {
  res.send('Express + TypeScript Server');
});

app.listen(3001, () => {
  console.log('Server is running at http://localhost:3001');
});
```

  </template>
  <template #right>

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:3002");

app.MapGet("/", () => ".NET Minimal Web API");

app.Run();
```

  </template>
</CodeSplitter>

::: tip Progressively enable HTTP application server features as needed
From here, it is easy to extend .NET's minimal API to include request filtering, CORS, authentication and authorization, and much, much more just by progressively turning features on and off (no need to add 3rd party packages for core functionality).  Check out [**the official quick reference**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0) for how to work with minimal APIs if you prefer this style!
:::

## Performance

How do they stack up?

![](../../assets/techempower.png)

<CodeSplitter>
  <template #left>

```ts
// 113,117; See reference link below
app.get("/plaintext", (req, res) => {
  writeResponse(res, GREETING, headerTypes["plain"]);
});

// 92,604
app.get("/json", (req, res) => {
  writeResponse(res, jsonSerializer({ message: GREETING }));
});
```

  </template>
  <template #right>

```csharp
// 7,014,298; See reference link below
app.MapGet("/plaintext", () => "Hello, World!");

// 1,042,029
app.MapGet("/json", () => new { message = "Hello, World!" });
```

  </template>
</CodeSplitter>

Implementation:

- [C# plaintext](https://github.com/TechEmpower/FrameworkBenchmarks/blob/master/frameworks/CSharp/aspnetcore/src/Minimal/Program.cs#L29)
- [Express plaintext](https://github.com/TechEmpower/FrameworkBenchmarks/blob/master/frameworks/JavaScript/express/src/server.mjs#L23C1-L25C4)

## Packaging for Deployment

<CodeSplitter>
  <template #left>

```shell
# Use alpine for size, but feel free to use other builds if running into issues.
FROM node:20-alpine
WORKDIR /usr/src/app

# Copy over assets
COPY package.json ./
COPY package-lock.json ./

# Install dependencies.
RUN npm ci

# Copy source
COPY . .

# Build the TypeScript
RUN npx tsc

# Start the server.
EXPOSE 3001
CMD ["node", "dist/index.js"]

# From src/typescript/express-app
# ✅ docker buildx build -t ts/express-web-api -f ./Dockerfile .
```

  </template>
  <template #right>

```shell
# Build layer
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Our project layer so we only update on new deps
COPY ./webapi-minimal.csproj ./webapi-minimal.csproj

# Restore dependencies
RUN dotnet restore

# Copy over code and publish
COPY ./Program.cs ./Program.cs

# Build the binaries
RUN dotnet publish ./webapi-minimal.csproj -o /app/published-app --configuration Release

# Runtime layer
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENTRYPOINT [ "dotnet", "/app/webapi-minimal.dll" ]

# From src/csharp/webapi-minimal
# ✅ docker buildx build -t cs/minimal-web-api -f ./Dockerfile .
```

  </template>
</CodeSplitter>

::: tip Should startups use .NET?
It seems that it is easier to build a fully functional web API backend with .NET than it is with Express.js where teams need to not only pick 3rd party packages to get a functional backend API server, but also need to carefully evaluate their production worthiness.  With .NET's "batteries included" philosophy, teams primarily "turn on" features progressively as needed.

.NET is easier to use and performs better!
:::
