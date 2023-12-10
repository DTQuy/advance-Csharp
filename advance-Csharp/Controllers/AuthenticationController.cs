using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Response.Authentication;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authService;

        public AuthController(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationRegisterRequest request)
        {
            try
            {
                AuthenticationRegisterResponse response = await authService.RegisterUser(request);

                return response != null && response.Message.Contains("Successfully") ? Ok(response) : BadRequest(response);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        /*[HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationLoginRequest request)
        {
            try
            {
                AuthenticationLoginResponse response = await authService.AccountAuthentication(request);

                return response != null && response.Token != null ? Ok(response) : BadRequest(response);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }*/
    }
}