using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Response.Authentication;
using advance_Csharp.Service.Authorization;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }
        [HttpPost("Authentication")]
        [Service.Authorization.AllowAnonymous]
        public async Task<ActionResult<AuthenticationLoginResponse>> Authenticate([FromQuery] AuthenticationLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Kiểm tra _authenticationService có giá trị không null
                if (_authenticationService == null)
                {
                    // Trả về 500 Internal Server Error nếu _authenticationService là null
                    return StatusCode(500, "Authentication service is not available");
                }

                AuthenticationLoginResponse loginResponse = await _authenticationService.AccountAuthentication(request);

                // Kiểm tra thuộc tính Success hoặc cách khác để xác định thành công
                return loginResponse.Success ? Ok(loginResponse) : BadRequest(loginResponse.Message);
            }
            catch (Exception ex)
            {
                // Logging thông báo lỗi
                Console.WriteLine(ex.Message);

                // Trả về 500 Internal Server Error và thông báo lỗi
                return StatusCode(500, ex.Message);
            }
        }


    }
}
