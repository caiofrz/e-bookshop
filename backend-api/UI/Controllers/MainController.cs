using Microsoft.AspNetCore.Mvc;

namespace backend_api.UI.Controllers;

[ApiController]
[Route("api/")]
public class MainController : ControllerBase
{
    [HttpGet]
    public IActionResult Index() => Ok("Olá, Mundo!");
}