using Microsoft.AspNetCore.Mvc;

namespace TryCsharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController(
  ILogger<AppController> logger,
  AppService appService
) : ControllerBase {
  [HttpGet()]
  public string GetHello() => appService.GetHello();

  [HttpGet("increment/{count}")]
  public string Increment(int count) => $"You added: ${count}";
}
