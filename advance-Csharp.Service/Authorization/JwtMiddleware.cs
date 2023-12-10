using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace advance_Csharp.Service.Authorization
{
    public class JwtMiddleware
    {
        /// <summary>
        /// RequestDelegate
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// JwtMiddleware
        /// </summary>
        /// <param name="next"></param>
        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;

        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <param name="jwtService"></param>
        /// <returns></returns>
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
