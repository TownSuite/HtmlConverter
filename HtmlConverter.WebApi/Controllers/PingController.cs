using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

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
