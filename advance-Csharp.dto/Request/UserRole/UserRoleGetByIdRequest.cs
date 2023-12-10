namespace advance_Csharp.dto.Request.UserRole
{
    public class UserRoleGetByIdRequest
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
