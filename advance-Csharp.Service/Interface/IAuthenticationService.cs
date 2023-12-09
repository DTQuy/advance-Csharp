using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Response.Authentication;

namespace advance_Csharp.Service.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationRegisterResponse> RegisterUser(AuthenticationRegisterRequest Request);
        Task<AuthenticationLoginResponse> AccountAuthentication(AuthenticationLoginRequest Request);
    }
}
