using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QA.Workflow.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            return Ok("api works");
        }
    }
}
