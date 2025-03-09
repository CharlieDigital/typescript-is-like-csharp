using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EFExample.Data;

public record DbConfig(string ConnectionString);

public class Database(DbConfig config) : DbContext {
  public DbSet<Runner> Runners { get; set; } = null!;
  public DbSet<Race> Races { get; set; } = null!;

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    if (optionsBuilder.IsConfigured){
      return;
    }

    optionsBuilder
      .UseNpgsql(config.ConnectionString, o => o.UseAdminDatabase("postgres"))
      .UseSnakeCaseNamingConvention()
      .EnableDetailedErrors()
      .EnableSensitiveDataLogging();
  }
}

[Index(nameof(Email))]
public class Runner {
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Country { get; set; }
  [JsonIgnore]
  public List<Race> Races { get; set; } = [];
  [JsonIgnore]
  public List<RaceResult> RaceResults { get; set; } = [];
}

[Index(nameof(Date))]
public class Race {
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }
  public required string Name { get; set; }
  public required DateTime Date { get; set;}
  public required decimal DistanceKm { get; set; }
  [JsonIgnore]
  public List<Runner> Runners { get; set; } = [];
  [JsonIgnore]
  public List<RaceResult> RaceResults { get; set; } = [];
}

[Index(nameof(RunnerId), nameof(RaceId))]
[Index(nameof(BibNumber))]
public class RaceResult {
  public int Id { get; set; }
  public int RunnerId { get; set; }
  public int RaceId { get; set; }
  public Runner Runner { get; set; } = null!;
  public int BibNumber { get; set; }
  public Race Race { get; set; } = null!;
  public int Position { get; set; }
  public TimeSpan Time { get; set; }
}
