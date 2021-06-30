using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlConverter.WebApi.Controllers
{
    [ApiController]
    [Route("/ping")]
    [Route("v{v:apiVersion}/ping")]
    [ApiVersion("1.0")]
    public class PingController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Pong";
        }
    }
}
