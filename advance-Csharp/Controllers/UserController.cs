using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using advance_Csharp.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    // <summary>
    ///  Controller api user 
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// User Controller
        /// </summary>
        public UserController()
        {
            _userService = new UserService();
        }

        /// <summary>
        /// get-user-admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("get-user-admin")]
        [HttpGet()]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> GetUserAdmin([FromQuery] UserGetListRequest request)
        {
            try
            {
                UserGetListResponse response = await _userService.GetApplicationUserList(request);
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
        /// Create User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("create-user-admin")]
        [HttpPost]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            try
            {
                UserCreateResponse response = await _userService.CreateUser(request);
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
        /// update-User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("update-user-admin")]
        [HttpPut]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            try
            {
                UserUpdateResponse response = await _userService.UpdateUser(request);

                return response.OldUser == null || response.UpdatedUser == null ? NotFound(response.Message) : Ok(response);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete-user-admin")]
        [HttpDelete]
        [MyAppAuthentication("Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                UserDeleteResponse response = await _userService.DeleteUser(id);

                return response.DeletedUser == null ? NotFound(response.Message) : Ok(response);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
