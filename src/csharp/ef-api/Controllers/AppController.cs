using EFExample.Data;
using Microsoft.AspNetCore.Mvc;

namespace EFExample.Controllers;

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

  [HttpGet("/results/{email}")]
  public async Task<RunnerResults> GetRunnerResults(string email) {
    var result = await resultsRepository.RunnerResults(email);
    return new(
      result,
      [..result.RaceResults ?? []],
      [..result.Races ?? []]
    );
  }
}
public record RunnerResults(
  Runner Runner,
  RaceResult[] Results,
  Race[] Races
);
