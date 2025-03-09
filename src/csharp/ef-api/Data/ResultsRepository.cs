using Microsoft.EntityFrameworkCore;

namespace EFExample.Data;

public record RunnerRaceResult(
  string RunnerName,
  string RaceName,
  int Position,
  TimeSpan Time,
  DateTime RaceDate
);

public class ResultsRepository(
  Database db // 👈 Injected via DI
) {
  public async Task<IEnumerable<RunnerRaceResult>> Top10FinishesByRunner(string email)
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
