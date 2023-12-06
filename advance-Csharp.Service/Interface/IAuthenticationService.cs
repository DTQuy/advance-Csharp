using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Service.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationLoginResponse> AccountAuthentication(AuthenticationLoginRequest Request);
    }
}
