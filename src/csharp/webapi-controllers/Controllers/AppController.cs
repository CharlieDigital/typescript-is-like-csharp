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
}
