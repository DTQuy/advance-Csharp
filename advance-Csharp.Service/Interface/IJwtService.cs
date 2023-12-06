using advance_Csharp.dto.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Service.Interface
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(UserResponse userResponse);
        Guid? ValidateAccessToken(string token);
    }
}
