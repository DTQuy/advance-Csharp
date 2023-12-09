using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace advance_Csharp.Service.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;

        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();

            if (!string.IsNullOrEmpty(token))
            {
                Guid? userId = jwtService.ValidateAccessToken(token);
                if (userId.HasValue)
                {
                    UserGetByIdRequest userGetByIdRequest = new()
                    {
                        Id = userId.Value,
                    };

                    // Attach user to context on successful JWT validation
                    UserGetByIdResponse userGetByIdResponse = await userService.GetUserById(userGetByIdRequest);
                    if (userGetByIdResponse != null)
                    {
                        context.Items["User"] = userGetByIdResponse.Data;
                    }
                }
            }

            await next(context);
        }
    }
}
