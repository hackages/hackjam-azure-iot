using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamp.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lamp.Controllers
{
    [Route("/")]
    public class LampController : ControllerBase
    {
        public IActionResult Index([FromServices] LampState state)
        {
            return Ok(state);
        }
    }
}
