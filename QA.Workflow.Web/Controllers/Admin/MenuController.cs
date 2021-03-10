using Microsoft.AspNetCore.Mvc;
using QA.Workflow.Business.Interfaces.Admin;
using System.Threading.Tasks;

namespace QA.Workflow.Web.Controllers.Admin
{
    [Route("api/mn")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuService _menuMasterService;

        public MenuController(IMenuService menuMasterService)
        {
            _menuMasterService = menuMasterService;
        }

        /// <summary>
        /// Get All Menu Async
        /// </summary>
        /// <returns></returns>
        [HttpGet("menulist")]
        public async Task<ActionResult> GetAllMenuAsync()
        {
            return Ok(await _menuMasterService.GetAllMenuAsync());
        }
    }
}
