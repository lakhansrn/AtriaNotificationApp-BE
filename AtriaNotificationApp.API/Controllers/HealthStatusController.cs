using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/healthStatus")]
    [ApiController]
    public class HealthStatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetHealth()
        {
            return Ok();
        }
    }
}