using advance_Csharp.dto.Request.Role;
using advance_Csharp.dto.Response.Role;

namespace advance_Csharp.Service.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// SearchRole
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RoleSearchResponse> SearchRole(RoleSearchRequest request);
    }
}
