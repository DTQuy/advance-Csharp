using advance_Csharp.dto.Request.Role;
using advance_Csharp.dto.Response.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Service.Interface
{
    public interface IRoleService
    {
        Task<RoleSearchResponse> SearchRole(RoleSearchRequest request);
    }
}
