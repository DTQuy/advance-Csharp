using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using advance_Csharp.Database.Models;
using Microsoft.AspNetCore.Http;

namespace advance_Csharp.Service.Authorization
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public string RoleName { get; set; }

        public CustomAuthorizeAttribute(string roleName)
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
                    // Check other conditions or logic if needed
                    return;
                }

                baseResponse.Message = "User is unauthorized";

                context.Result = new JsonResult(baseResponse) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
