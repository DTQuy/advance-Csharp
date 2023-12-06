using advance_Csharp.Database.Models;

namespace advance_Csharp.dto.Response.UserRole
{
    public class UserRoleGetByIdResponse
    {
        /// <summary>
        /// massage
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// User Response
        /// </summary>
        public UserRoleResponse? UserResponse { get; set; }
    }
}
