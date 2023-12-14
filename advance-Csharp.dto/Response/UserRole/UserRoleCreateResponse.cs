namespace advance_Csharp.dto.Response.UserRole
{
    public class UserRoleCreateResponse
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
