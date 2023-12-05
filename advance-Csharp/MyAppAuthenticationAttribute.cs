using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace advance_Csharp
{
    public class MyAppAuthenticationAttribute : ActionFilterAttribute
    {
        public string Role;

        public MyAppAuthenticationAttribute(string role)
        {
            Role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!string.IsNullOrEmpty(Role))
            {
                if (Role != "Admin")
                {
                    context.Result = new UnauthorizedObjectResult("user is unauthorized");
                }
            }
        }
    }
}
