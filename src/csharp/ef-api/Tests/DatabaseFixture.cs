using EFExample.Data;

namespace EFExample.Tests;

public class DatabaseFixture
{
  private static readonly string _connectionString = "server=127.0.0.1;port=5432;database=ef;user id=postgres;password=postgres;include error detail=true;";
  private static readonly object _sync = new();
  private static bool _initialized;

  /// <summary>
  ///     Constructor which initializes the database for testing each run.
  /// </summary>
  public DatabaseFixture() {
    // Use simple double lock-check mechanism.
    if (_initialized) {
      return;
    }

    lock (_sync) {
      if (_initialized) {
        return;
      }

      using var db = CreateDbContext();
      db.Database.EnsureDeleted();
      db.Database.EnsureCreated();
      _initialized = true;
    }
  }

  public static Database CreateDbContext() {
    return new Database(new DbConfig(_connectionString));
  }
}
