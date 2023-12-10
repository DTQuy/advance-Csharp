using advance_Csharp.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace advance_Csharp.Service.Authorization
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// RoleName
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// CustomAuthorizeAttribute
        /// </summary>
        /// <param name="roleName"></param>
        public CustomAuthorizeAttribute(string roleName)
        {
            RoleName = roleName;
        }

        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="context"></param>
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
