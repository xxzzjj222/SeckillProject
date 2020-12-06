using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.UserServices.Controllers
{
    [Route("HealthCheck")]
    [ApiController]
    public class HealthCheckController : Controller
    {

        [HttpGet ]
        public ActionResult GetHealthCheck()
        {
            return Ok("连接正常");
        }
    }
}
