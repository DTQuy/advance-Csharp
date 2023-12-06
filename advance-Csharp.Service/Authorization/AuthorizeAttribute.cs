using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using advance_Csharp.Database.Models;
using Microsoft.AspNetCore.Http;

namespace advance_Csharp.Service.Authorization
{
    public class AuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public string RoleName { get; set; }

        public AuthorizeAttribute(string roleName)
        {
            RoleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }

            BaseResponse baseResponse = new();

            if (!string.IsNullOrEmpty(RoleName))
            {
                if (RoleName == "Admin")
                {
                    return;
                }

                baseResponse.Message = "User is unauthorized";

                context.Result = new JsonResult(baseResponse) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
