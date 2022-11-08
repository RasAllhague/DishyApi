using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet()]
        public string Get()
        {
            return "0.1.0";
        }
    }
}
