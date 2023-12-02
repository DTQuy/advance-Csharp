using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;

namespace advance_Csharp.Service.Interface
{
    public interface IUserSevice
    {
        /// <summary>
        /// Get application by Email string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGetListResponse> GetApplicationUserList(UserGetListRequest request);
    }
}
