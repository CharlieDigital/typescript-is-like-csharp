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
}
