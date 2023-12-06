using advance_Csharp.dto.Request.UserRole;
using advance_Csharp.dto.Response.UserRole;
using System.Threading.Tasks;

namespace advance_Csharp.Service.Interface
{
    public interface IUserRoleService
    {
        Task<UserRoleCreateResponse> AddUserRole(UserRoleCreateRequest request);

        Task<UserRoleGetByIdResponse> UserRoleGetById(UserRoleGetByIdRequest request);
    }
}
