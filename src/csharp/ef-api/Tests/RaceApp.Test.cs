using EFExample.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFExample.Tests;

/**
* Special note: this is not common practice to place unit tests inline with code
* when it comes to C# and .NET because the build process, by default, will include
* these files into the output runtime assembly.  We can exclude these using
* some configuration (see the .csproj for details)
*
* I've included these here inline with the project source (rather than a separate
* project as would normally be the case) because this is how Node.js developers
* using Jest or Vitest might expect to see their unit tests
*/

public class TestRaceApp : IClassFixture<DatabaseFixture> {
  [Fact]
  public async Task Test_Add_Race() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    db.Races.Add(new () {
      Name = "New York City Marathon",
      Date = new DateTime(),
      DistanceKm = 42.195m
    });

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();
    var race = await db.Races.FirstAsync();

    Assert.NotNull(race);
  }

  [Fact]
  public async Task Test_Add_Runner() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    db.Runners.Add(new () {
      Name = "Ada Lovelace",
      Email = "ada@example.org",
      Country = "United Kingdom"
    });

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();
    var runner = await db.Runners.FirstAsync();

    Assert.NotNull(runner);
  }

  [Fact]
  public async Task Test_Add_Single_RaceResult() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

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
    db.ChangeTracker.Clear();
    var loadedRunner = await db.Runners
      .Include(r => r.RaceResults)
      .Include(r => r.Races)
      .FirstAsync();

    Assert.NotNull(loadedRunner);
    Assert.NotEmpty(loadedRunner.Races);
    Assert.NotEmpty(loadedRunner.RaceResults);
    Assert.True(loadedRunner.RaceResults[0].Time == TimeSpan.FromMinutes(120));
  }

  [Fact]
  public async Task Test_Add_Multiple_RaceResults() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedAda = await db.Runners
      .Include(r => r.RaceResults)
      .Include(r => r.Races)
      .FirstAsync(r => r.Email == "ada@example.org");

    var loadedAlan = await db.Runners
      .Include(r => r.RaceResults)
      .Include(r => r.Races)
      .FirstAsync(r => r.Email == "alan@example.org");

    Assert.NotNull(loadedAda);
    Assert.NotEmpty(loadedAda.Races);
    Assert.NotEmpty(loadedAda.RaceResults);
    Assert.Equal(3, loadedAda.RaceResults.Count);

    Assert.NotNull(loadedAlan);
    Assert.NotEmpty(loadedAlan.Races);
    Assert.NotEmpty(loadedAlan.RaceResults);
    Assert.Equal(2, loadedAlan.RaceResults.Count);
  }

  [Fact]
  public async Task Test_Filter_Multiple_RaceResult_Multiple_Conditions() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedRunners = await db.Runners
      .Where(r => r.Name.StartsWith("Ada"))
      .Where(r => r.Name == "Alan Turing") // logical And
      .ToListAsync();

    Assert.Empty(loadedRunners);

    loadedRunners = await db.Runners
      .Where(r => r.Name.StartsWith("Ada") && r.Name.StartsWith("Alan"))
      .ToListAsync();

    Assert.Empty(loadedRunners);

    loadedRunners = await db.Runners
      .Where(r => r.Name.StartsWith("Ada") || r.Name.StartsWith("Alan"))
      .ToListAsync();

    Assert.NotEmpty(loadedRunners);
    Assert.Equal(2, loadedRunners.Count);
  }

  [Fact]
  public async Task Test_Filter_Multiple_RaceResults_By_Top_Ten_Finish() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedRunners = await db.Runners
      .Where(r => r.RaceResults.Where(
        finish => finish.Position <= 10
          && finish.Time <= TimeSpan.FromHours(2)
          && finish.Race.Name.Contains("New")
        ).Any()
      ).ToListAsync();

    Assert.NotEmpty(loadedRunners);
    Assert.Single(loadedRunners);;
    Assert.Equal(ada.Name, loadedRunners[0].Name);
  }

  [Fact]
  public async Task Test_Filter_Multiple_RaceResults_By_Top_Ten_Finish_With_Navigation() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedAda = await db.Runners
      .Include(r => r.RaceResults!.Where(
        finish => finish.Position <= 10
          && finish.Time <= TimeSpan.FromHours(2)
          && finish.Race.Name.Contains("New")
        )
      )
      .FirstAsync(r => r.Email == "ada@example.org");

    Assert.NotNull(loadedAda);
    Assert.NotNull(loadedAda.RaceResults);
    Assert.Single(loadedAda.RaceResults);
  }

  [Fact]
  public async Task Test_Filter_Multiple_RaceResults_By_Top_Ten_Finish_With_Projection() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedAdasTop10Races = await db.Runners
      .Where(r => r.Email == "ada@example.org")
      .SelectMany(r => r.RaceResults!.Where(
        finish => finish.Position <= 10)
      )
      .Select(finish => new {
        Runner = finish.Runner.Name,
        Race = finish.Race.Name,
        finish.Position,
        finish.Time
      })
      .OrderBy(r => r.Position)
      .ToListAsync();

    Assert.NotNull(loadedAdasTop10Races);
    Assert.NotEmpty(loadedAdasTop10Races);
    Assert.Equal(2, loadedAdasTop10Races.Count);
    Assert.Equal(
      ("Ada Lovelace", "New York City Marathon", 1, TimeSpan.FromMinutes(120)),
      (
        loadedAdasTop10Races[0].Runner,
        loadedAdasTop10Races[0].Race,
        loadedAdasTop10Races[0].Position,
        loadedAdasTop10Races[0].Time
      )
    );
    Assert.Equal(
      ("Ada Lovelace", "Boston Marathon", 5, TimeSpan.FromMinutes(145)),
      (
        loadedAdasTop10Races[1].Runner,
        loadedAdasTop10Races[1].Race,
        loadedAdasTop10Races[1].Position,
        loadedAdasTop10Races[1].Time
      )
    );
  }

  [Fact]
  public async Task Test_Filter_Multiple_RaceResults_By_Top_Ten_Finish_With_Projection_To_Record() {
    var db = DatabaseFixture.CreateDbContext();

    using var tx = await db.Database.BeginTransactionAsync();

    var ((ada, alan), _) = ProduceModel();

    db.AddRange(ada, alan);

    await db.SaveChangesAsync();
    db.ChangeTracker.Clear();

    var loadedAdasTop10Races = (await db.Runners
      .Where(r => r.Email == "ada@example.org")
      .SelectMany(r => r.RaceResults!.Where(
        finish => finish.Position <= 10)
      )
      .Select(finish => new {
        RunnerName = finish.Runner.Name,
        RaceName = finish.Race.Name,
        finish.Position,
        finish.Time
      })
      .OrderBy(r => r.Position)
      .ToListAsync())
      .Select(r => new TestRaceResult(
        r.RunnerName,
        r.RaceName,
        r.Position,
        r.Time)
      ).ToList();

    Assert.NotNull(loadedAdasTop10Races);
    Assert.NotEmpty(loadedAdasTop10Races);
    Assert.Equal(2, loadedAdasTop10Races.Count);
    Assert.Equal(
      ("Ada Lovelace", "New York City Marathon", 1, TimeSpan.FromMinutes(120)),
      (
        loadedAdasTop10Races[0].RunnerName,
        loadedAdasTop10Races[0].RaceName,
        loadedAdasTop10Races[0].Position,
        loadedAdasTop10Races[0].Time
      )
    );
    Assert.Equal(
      ("Ada Lovelace", "Boston Marathon", 5, TimeSpan.FromMinutes(145)),
      (
        loadedAdasTop10Races[1].RunnerName,
        loadedAdasTop10Races[1].RaceName,
        loadedAdasTop10Races[1].Position,
        loadedAdasTop10Races[1].Time
      )
    );
  }
  public record TestRaceResult(
    string RunnerName,
    string RaceName,
    int Position,
    TimeSpan Time
  );

  /// <summary>
  /// Utility method that produces a sample dataset to persist.
  /// </summary>
  private (
    (Runner Ada, Runner Alan) Runners,
    (Race Nyc, Race Bos, Race B135, Race H100, Race Spr) Races
  ) ProduceModel() {
    var ada = new Runner() {
      Name = "Ada Lovelace",
      Email = "ada@example.org",
      Country = "United Kingdom"
    };

    var alan = new Runner() {
      Name = "Alan Turing",
      Email = "alan@example.org",
      Country = "United Kingdom"
    };

    var ny = new Race() {
      Name = "New York City Marathon",
      Date = new DateTime(),
      DistanceKm = 42.195m
    };

    var bos = new Race() {
      Name = "Boston Marathon",
      Date = new DateTime(),
      DistanceKm = 42.195m
    };

    var b135 = new Race() {
      Name = "Badwater 135",
      Date = new DateTime(),
      DistanceKm = 217m
    };

    var h100 = new Race() {
      Name = "Hardrock 100",
      Date = new DateTime(),
      DistanceKm = 160.9m
    };

    var spr = new Race() {
      Name = "Spartathlon",
      Date = new DateTime(),
      DistanceKm = 246m
    };

    ada.Races = [ny, bos, b135];
    alan.Races = [h100, spr];

    // Ada's results
    var result_ny = new RaceResult() {
      Runner = ada,
      Race = ny,
      BibNumber = 1,
      Position = 1,
      Time = TimeSpan.FromMinutes(120)
    };

    var result_bos = new RaceResult() {
      Runner = ada,
      Race = bos,
      BibNumber = 1,
      Position = 5,
      Time = TimeSpan.FromMinutes(145)
    };

    var result_b135 = new RaceResult() {
      Runner = ada,
      Race = b135,
      BibNumber = 1,
      Position = 15,
      Time = TimeSpan.FromMinutes(820)
    };

    // Alan's results
    var result_h100 = new RaceResult() {
      Runner = alan,
      Race = h100,
      BibNumber = 1,
      Position = 15,
      Time = TimeSpan.FromMinutes(700)
    };

    var result_spr = new RaceResult() {
      Runner = alan,
      Race = spr,
      BibNumber = 1,
      Position = 9,
      Time = TimeSpan.FromMinutes(840)
    };

    ada.RaceResults = [result_ny, result_bos, result_b135];
    alan.RaceResults = [result_h100, result_spr];

    return(
      (ada, alan),
      (ny, bos, b135, h100, spr)
    );
  }
}
