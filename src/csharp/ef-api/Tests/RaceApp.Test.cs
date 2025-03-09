using EFExample.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFExample.Tests;

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
      Time = TimeSpan.FromMinutes(125)
    };

    var result_b135 = new RaceResult() {
      Runner = ada,
      Race = b135,
      BibNumber = 1,
      Position = 15,
      Time = TimeSpan.FromMinutes(820)
    };

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
      Position = 15,
      Time = TimeSpan.FromMinutes(840)
    };

    ada.RaceResults = [result_ny, result_bos, result_b135];
    alan.RaceResults = [result_h100, result_spr];

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
}
