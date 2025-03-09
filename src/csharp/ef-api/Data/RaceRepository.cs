using Microsoft.EntityFrameworkCore;

namespace EFExample.Data;

public class RaceRepository(Database db) {
  public async Task Add(Race race) => await db.AddAsync(race);

  public async Task<IEnumerable<Race>> GetAll() => await db.Races.ToListAsync();
}
