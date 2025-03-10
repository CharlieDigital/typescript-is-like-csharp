# Databases and ORMs

For both C# and TypeScript, there are a number of ORMs available to help teams interface with databases more productively.  ORMs typically provide features such as binding of query results to domain models, simplifying the read and write of records from and to the database, as well as handling schema migrations.  Microsoft ships a first-party ORM -- [**Entity Framework Core (EF Core)**](https://github.com/dotnet/efcore) -- which is mature and has a large number of database providers supported.  However, there are third party libraries available including [**NHibernate**](https://github.com/nhibernate/nhibernate-core) (and [**FluentNHibernate**](https://github.com/nhibernate/fluent-nhibernate)) as well as [**Dapper**](https://github.com/DapperLib/Dapper) (which is not a full ORM)

The Node.js ecosystem has a large number of NPM packages available including [**Prisma**](https://github.com/prisma/prisma) (14m+ downloads, ~42k stars), [**TypeORM**](https://github.com/typeorm/typeorm) (9m+ downloads, ~35k stars), [**Sequelize**](https://github.com/sequelize/sequelize) (9m+ downloads, ~30k stars), and [**Drizzle**](https://github.com/drizzle-team/drizzle-orm) (4m+ downloads, ~27k stars).  There are also query builder libraries like [**Kysely**](https://github.com/kysely-org/kysely) (2m+ downloads, ~12k stars) which perhaps maps more closely to a **Dapper** than a true ORM.

We'll compare the most popular Node.js library, Prisma, against EF Core and see where they align and where they differ.  We'll also see how the usage of LINQ in EF enables much more fluid query building.

::: warning Start the Postgres Docker instance first
Before running the examples and unit tests, start the Postgres container instance via `docker compose up`.

See the `docker-compose.yaml` file for more details.  The databases are dropped and re-created on each run.
:::

## Setting Up

Here, we'll create a simple API app using Nest.js and .NET controller web APIs along with unit tests to demonstrate how these ORMs work and how they plug into the DI system of common platform runtimes.

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```shell
# Add our packages in /src/csharp/ef-api
cd src/csharp
mkdir ef-api
cd ef-api
dotnet new webapi --use-controllers --no-https

# Add Pg driver for Entity Framework
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

# Add to allow snake_case naming (because Pg and caps are no fun)
dotnet add package EFCore.NamingConventions

# Add for migrations
dotnet add package Microsoft.EntityFrameworkCore.Design

# Add for unit tests
dotnet add package XUnit
```

  </template>
</CodeSplitter>

::: tip Source code
💡 The source code for this walkthrough [is available in GitHub](https://github.com/CharlieDigital/typescript-is-like-csharp/tree/main/src/csharp/ef-api).
:::

## Wiring Postgres

Next, we wire our Postgres driver and connect it to our runtime.

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp{24,25,29-34}
// 📄 Database.cs
// This is just for us to inject the connection string (and other config)
public record DbConfig(string ConnectionString);

// This is our database.  The key is to inherit from DbContext
public class Database(DbConfig config) : DbContext {
  // This method gets called on startup and we'll configure our database
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    if (optionsBuilder.IsConfigured){
      return;
    }

    optionsBuilder
      .UseNpgsql(config.ConnectionString, o => o.UseAdminDatabase("postgres"))
      .UseSnakeCaseNamingConvention()
      .EnableDetailedErrors() // ⚠️ ONLY DEV CODE
      .EnableSensitiveDataLogging(); // ⚠️ ONLY DEV CODE
  }
}

// 📄 Program.cs
var connectionString = "server=127.0.0.1;port=5432;database=momo;user id=postgres;password=postgres;include error detail=true;";

builder.Services.AddSingleton(new DbConfig(connectionString));
builder.Services.AddDbContext<Database>();

var app = var app = builder.Build();

// ⚠️ ONLY DEMO CODE; NOT FOR PROD ⚠️
// Get our database from DI and ensure we create it
// (because our Docker container is ephemeral)
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<Database>()!;
db.Database.EnsureCreated();
```

  </template>
</CodeSplitter>

::: warning This is just demo code!
Note that the last block of code is just demo code!  You do not need to do this in normal apps.  Here we are just applying the schema at startup for the purposes of this demo.
:::

## Creating a Schema

Let's see how we define a schema for each platform.

::: tip Keep an eye out for types
As you to through this series of exercises, keep an eye out for how the EF Core examples allow types to flow through the entire chain, preventing errors and mistakes at dev, build, *and* runtime.

At no point in these examples does EF require usage of strings to reference properties, operations, and so on.
:::

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp{3,4}
public class Database(DbConfig config) : DbContext {
  // 👇 These two define our schema
  public DbSet<Runner> Runners { get; set; } = null!;
  public DbSet<Race> Races { get; set; } = null!;

  // ℹ️ Note that we don't map the relation table directly

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    // Snipped...
  }
}

// 🏃‍♀️ Runners
[Index(nameof(Email))]
public class Runner {
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Country { get; set; }
  [JsonIgnore] // 👇 Do not serialize this to JSON
  public List<Race>? Races { get; set; }
  [JsonIgnore] // 👇 Do not serialize this to JSON
  public List<RaceResult>? RaceResults { get; set; }
}

// 🏎️ Races
[Index(nameof(Date))]
public class Race {
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public required string Name { get; set; }
  public required DateTime Date { get; set; }
  public required decimal DistanceKm { get; set; }
  [JsonIgnore] // 👇 Do not serialize this to JSON
  public List<Runner>? Runners { get; set; }
  [JsonIgnore] // 👇 Do not serialize this to JSON
  public List<RaceResult>? RaceResults { get; set; }
}

// 🥇 Results (maps many-to-many)
// This is an implicit table that we don't access directly but
// through the navigation of the properties
[PrimaryKey(nameof(RunnerId), nameof(RaceId))]
[Index(nameof(BibNumber))]
public class RaceResult {
  public int RunnerId { get; set; }
  public int RaceId { get; set; }
  public Runner Runner { get; set; } = null!;
  public int BibNumber { get; set; }
  public Race Race { get; set; } = null!;
  public int Position { get; set; }
  public TimeSpan Time { get; set; }
}
```

  </template>
</CodeSplitter>

::: tip Data annotations for mapping and data quality
Here, we see some basic data annotations to specify indices and primary keys.  **[EF Core data annotations](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt)** allow customization of the schema mapping.  If you prefer more explicitness, you can also use [**fluent configuration**](https://learn.microsoft.com/en-us/ef/core/modeling/#use-fluent-api-to-configure-a-model) instead.  Combine these with [**.NET web API data annotations**](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-9.0), .NET offers a painless way to handle most common data validation use cases.
:::

## Writing Data

### Adding a Single Record

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp
db.Races.Add(new () {
  Name = "New York City Marathon",
  Date = new DateTime(),
  DistanceKm = 42.195m
});

await db.SaveChangesAsync();
```

  </template>
</CodeSplitter>

### Adding Complex Relations

Here, we create a runner, a race, and a result for the runner and race.

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp
var runner = new Runner() {
  Name = "Ada Lovelace",
  Email = "ada@example.org",
  Country = "United Kingdom"
};

var race = new Race() {
  Name = "New York City Marathon",
  Date = new DateTime(),
  DistanceKm = 42.195m
};

runner.Races = [race];

var result = new RaceResult() {
  Runner = runner,
  Race = race,
  BibNumber = 1,
  Position = 1,
  Time = TimeSpan.FromMinutes(120)
};

runner.RaceResults = [result];

db.Add(runner);

await db.SaveChangesAsync();
```

  </template>
</CodeSplitter>

## Reading Data

Here, we'll see how .NET's [Language Integrated Query (LINQ)](./linq.md) libraries make querying databases feel fluid through the use of a fluent, functional DX.

### Basic Reads

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp
// Composing Where clauses
var loadedRunners = await db.Runners
  .Where(r => r.Name.StartsWith("Ada"))
  .Where(r => r.Name == "Alan") // logical And
  .ToListAsync();
  // ✅ 0 results

// Here we see .NET Expressions in action because
// we can actually read this equality expression at runtime
// and break it down.  Cool! 😎

// Still 0 results; same query:
loadedRunners = await db.Runners
  .Where(r => r.Name.StartsWith("Ada")
    && r.Name.StartsWith("Alan")
  )
  .ToListAsync();
  // ✅ 0 results

// 2 results
loadedRunners = await db.Runners
  .Where(r => r.Name.StartsWith("Ada")
    || r.Name.StartsWith("Alan")
  )
  .ToListAsync();
  // ✅ 2 results
```

  </template>
</CodeSplitter>

::: tip .NET [Expression trees](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/)
The logical expressions in the examples above should stand out because here, we've directly used the language level equality expression. *Expression trees* allow us to *evaluate the expression* at runtime to break apart its component structures to build the underlying SQL query (instead of using strings).
:::

### Read with Navigation Includes

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp
// Read the runners and include the navigation properties
var loadedRunner = await db.Runners
  .Include(r => r.RaceResults)
  .Include(r => r.Races)
  .ToListAsync();

// Read a specific runner with filtering
var loadedAda = await db.Runners
  .Include(r => r.RaceResults)
  .Include(r => r.Races)
  .FirstAsync(r => r.Email == "ada@example.org");
```

  </template>
</CodeSplitter>

### Complex Reads

<CodeSplitter>
  <template #left>

```ts

```

  </template>
  <template #right>

```csharp
// Read with a filter on the navigation for races where
// the runner finished in the top 10, 2 hours or less, and
// the race name contained the word "New"
var loadedAda = await db.Runners
  .Include(r => r.RaceResults.Where(
    finish => finish.Position <= 10
      && finish.Time <= TimeSpan.FromHours(2)
      && finish.Race.Name.Contains("New")
    )
  )
  .FirstAsync(r => r.Email == "ada@example.org");
  // Only Ada (Runner + RaceResult populated)

// Same read, but we only want the runners (not their results)
var loadedRunners = await db.Runners
  .Where(r => r.RaceResults.Where(
    finish => finish.Position <= 10
      && finish.Time <= TimeSpan.FromHours(2)
      && finish.Race.Name.Contains("New")
    ).Any()
  ).ToListAsync();
  // Only Ada (Runner only)
```

  </template>
</CodeSplitter>

::: tip Expression trees are not evaluated
Here, the expression trees are not actually evaluated; they are only read to produce the equivalent SQL.
:::

## Projection

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp{9-14}
// Load Ada's top 10 races, order by finish position, and
// project the results
var loadedAdasTop10Races = await db.Runners
  .Where(r => r.Email == "ada@example.org")
  .SelectMany(r => r.RaceResults!.Where(
    finish => finish.Position <= 10)
  )
  // ✨ Notice how everything is fully typed downstack
  .Select(finish => new {
    Runner = finish.Runner.Name,
    Race = finish.Race.Name,
    finish.Position,
    finish.Time
  })
  .OrderBy(r => r.Position)
  .ToListAsync();

/*
* [
*  { Runner: "Ada Lovelace", Race: "New York City Marathon", Position: 1, Time: 00:02:00 }
*  { Runner: "Ada Lovelace", Race: "Boston Marathon", Position: 5, Time: 00:02:25 }
* ]
*/
```

  </template>
</CodeSplitter>

::: info Anonymous types in action
The result of the select from the .NET side is an [anonymous type](../basics/classes.md#anonymous-types).  For transmitting, it probably makes sense to convert this into a `Record`.
:::

## Writing a Repository

Let's examine how we create repositories and connect them to our DI to make them available to controllers and services.

::: info These implementations are not complete
See [**the unit tests in the repo**](https://github.com/CharlieDigital/typescript-is-like-csharp/blob/main/src/csharp/ef-api/Tests/RaceApp.Test.cs) for full implementations.  The purpose of the API implementations is purely to demonstrate how the DI works and how things get wired up.
:::

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp{42,51,56-60}
// 📄 ResultsRepository.cs: Sample repository
public class ResultsRepository(
  Database db // 👈 Injected via DI
) {
  public async Task<
    IEnumerable<RunnerRaceResult>
  > Top10FinishesByRunner(string email)
    => (await db.Runners
      .Where(r => r.Email == email)
      .SelectMany(r => r.RaceResults!.Where(
        finish => finish.Position <= 10)
      )
      // ✨ Notice how everything is fully typed downstack
      .Select(finish => new {
          RunnerName = finish.Runner.Name,
          RaceName = finish.Race.Name,
          finish.Position,
          finish.Time,
          RaceDate = finish.Race.Date
        }
      )
      .OrderBy(r => r.Position)
      .ToListAsync())
      .Select(r => new RunnerRaceResult(
        r.RunnerName,
        r.RaceName,
        r.Position,
        r.Time,
        r.RaceDate
      ));
}

public record RunnerRaceResult(
  string RunnerName,
  string RaceName,
  int Position,
  TimeSpan Time,
  DateTime RaceDate
);

// 📄 Program.cs: set up our DI
builder.Services.AddScoped<ResultsRepository>();
builder.Services.AddSingleton(new DbConfig(connectionString));
builder.Services.AddDbContext<Database>();

// 📄 AppController.cs: Add our endpoint and DI
[ApiController]
[Route("[controller]")]
public class AppController(
  ILogger<AppController> logger,
  ResultsRepository resultsRepository
) : ControllerBase {
  [HttpGet]
  public string Get() => "Hello, World!";

  [HttpGet("/top10/{email}")]
  public async Task<List<RunnerRaceResult>> GetTop10FinishesByRunner(string email) {
    var results = await resultsRepository.Top10FinishesByRunner(email);
    return [.. results];
  }
}
```

  </template>
</CodeSplitter>

## Hoisting Navigations

EF Core will attempt to persist the entire object tree if you round-trip the entity.  To prevent this -- for example, we only want to round-trip the runner -- we can use a simple technique here to split out the navigation collections from the results:

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```csharp{4,5,20-22}
// 📄 ResultsRepository.cs: Retrieve a runner and her results
public async Task<Runner> RunnerResults(string email)
  => await db.Runners
    .Include(r => r.RaceResults) // 👈 Included
    .Include(r => r.Races) // 👈 Included
    .FirstAsync(r => r.Email == email);

// We "hoist" our dependent properties here.
public record RunnerResults(
  Runner Runner,
  Races[] Races,
  RaceResult[] Results
);

// 📄 AppController.cs: Endpoint for runner and results
[HttpGet("/results/{email}")]
public async Task<RunnerResults> GetRunnerResults(string email) {
  var result = await resultsRepository.RunnerResults(email);
  return new(
    result,  // 👈 Will NOT have .Races and .RaceResults in JSON output
    [..result.RaceResults ?? []], // 👈 Hoisted
    [..result.Races ?? []]  // 👈 Hoisted
  );
}
```

  </template>
</CodeSplitter>

Remember how we used `[JsonIgnore]` in our model? This means that at serialization at the boundary, `Runner.Races` and `Runner.RaceResults` will automatically be stripped out (nice)!  So to keep them in the output JSON, we need to "hoist" them up into a "DTO" record.

::: tip
This is an extremely useful pattern and should generally be used for all navigation properties as it will allow round-tripping the entity for updates without passing the navigations along.
:::

## Adding Migrations

<CodeSplitter>
  <template #left>

```ts
// 🚧  WIP
```

  </template>
  <template #right>

```shell
# From /src/csharp/ef-api
dotnet ef migrations add Initial

# Generate idempotent SQL file (best for upstream deployment)
dotnet ef migrations script \
  --output Migrations/Scripted/migration.sql \
  --idempotent

# Apply updates
dotnet ef database update
```

  </template>
</CodeSplitter>

## Performance

I've captured results here using the Fortunes benchmark from TechEmpower from Round 23 (2025-02-04).  [The description of the Fortunes benchmark](https://github.com/TechEmpower/FrameworkBenchmarks/wiki/Project-Information-Framework-Tests-Overview) is as follows:

> Exercises the ORM, database connectivity, dynamic-size collections, sorting, server-side templates, XSS countermeasures, and character encoding.

The results have been filtered down to "full" ORM stacks to bypass partial ORMs (like Dapper) and raw data access.  The last two columns indicates that these results are "full ORM" and "realistic approach" for the platform.

![ORM performance](../../assets/orm-perf.png)

::: tip Think of these results as "throughput"
I think the practical way of thinking of these results as the volume of throughput for a given dollar amount in hardware spend; it's not about shaving 3ms off of the response time, it's responding to more clients using the same amount of infrastructure.

In other words, *"You'll spend up to 4x as much on application infrastructure with Nest.js to support the same volume of requests."*
:::

::: info Note on the Node.js ORMs
For Nest.js, the ORM used is TypeORM ([see source](https://github.com/TechEmpower/FrameworkBenchmarks/blob/master/frameworks/TypeScript/nest/package.json#L35)).  For Node.js, it's Sequelize ([see source](https://github.com/TechEmpower/FrameworkBenchmarks/blob/master/frameworks/JavaScript/nodejs/package.json#L17)).  For Express, it is also Sequelize ([see source](https://github.com/TechEmpower/FrameworkBenchmarks/blob/master/frameworks/JavaScript/express/package.json#L17)).

Prisma [is generally known to be lower throughput](https://github.com/geldata/imdbench?tab=readme-ov-file#javascript-orms-full-report), but is the most popular framework on Node.js
:::
