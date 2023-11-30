using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.Service;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    /// <summary>
    /// Api Controller Application
    /// </summary>
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private IApplicationService _ApplicationService;

        public ApplicationController()
        {
            _ApplicationService = new ApplicationService();
        }

        /// <summary>
        /// get-version
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("get-version")]
        [HttpGet()]
        [MyAppAuthentication("User")]
        public async Task<IActionResult> GetVersion([FromQuery] AppVersionGetListRequest request)
        {
            try
            {
                AppVersionGetListResponse response = await _ApplicationService.GetApplicationVersionList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // send to logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// get-version-admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("get-version-admin")]
        [HttpGet()]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> GetVersionAdmin([FromQuery] AppVersionGetListRequest request)
        {
            try
            {
                AppVersionGetListResponse response = await _ApplicationService.GetApplicationVersionList(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                // send to logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
