using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace advance_Csharp.Service.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public JwtMiddleware(RequestDelegate next, IUserService userService, IJwtService jwtService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();

            if (!string.IsNullOrEmpty(token))
            {
                Guid? userId = _jwtService.ValidateAccessToken(token);
                if (userId.HasValue)
                {
                    UserGetByIdRequest userGetByIdRequest = new()
                    {
                        Id = userId.Value,
                    };

                    // Attach user to context on successful JWT validation
                    UserGetByIdResponse userGetByIdResponse = await _userService.GetUserById(userGetByIdRequest);
                    if (userGetByIdResponse != null)
                    {
                        context.Items["User"] = userGetByIdResponse.Data; // Assuming 'Data' property contains user information
                    }
                }
            }

            await _next(context);
        }
    }
}
