namespace advance_Csharp.dto.Request.UserRole
{
    public class UserRoleCreateRequest
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
