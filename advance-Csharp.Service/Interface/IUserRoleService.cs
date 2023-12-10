using advance_Csharp.dto.Request.UserRole;
using advance_Csharp.dto.Response.UserRole;

namespace advance_Csharp.Service.Interface
{
    public interface IUserRoleService
    {
        /// <summary>
        /// AddUserRole
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRoleCreateResponse> AddUserRole(UserRoleCreateRequest request);

        /// <summary>
        /// GetUserRoleById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRoleGetByIdResponse> GetUserRoleById(UserRoleGetByIdRequest request);
    }
}
