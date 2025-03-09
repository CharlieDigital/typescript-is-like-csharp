using Microsoft.AspNetCore.Mvc;

namespace EFExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController(ILogger<AppController> logger) : ControllerBase {
  [HttpGet]
  public string Get() => "Hello, World!";
}
