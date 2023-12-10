using advance_Csharp.dto.Response.User;

namespace advance_Csharp.Service.Interface
{
    public interface IJwtService
    {
        /// <summary>
        /// GenerateAccessToken
        /// </summary>
        /// <param name="userResponse"></param>
        /// <returns></returns>
        Task<string> GenerateAccessToken(UserResponse userResponse);

        /// <summary>
        /// ValidateAccessToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Guid? ValidateAccessToken(string token);
    }
}
