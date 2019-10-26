using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Switch.Implementations;

namespace Switch.Controllers
{
    [Route("/")]
    [Controller]
    public class SwitchController : ControllerBase
    {
        public async Task<IActionResult> Toggle(
            [FromQuery] bool switchPosition, 
            [FromServices] SwitchImplementation handler)
        {
            await handler.HandleToggle(switchPosition);
            return Ok();
        }
    }
}
