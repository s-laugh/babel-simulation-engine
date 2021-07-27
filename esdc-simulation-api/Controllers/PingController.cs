using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace esdc_simulation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// Ping
        /// </summary>
        /// <returns>Welcome text with date</returns>
        [HttpGet]
        public ActionResult<string> Index()
        {
            return $"Welcome to the Simulation API: {DateTime.Now}";
        }
    }
}
