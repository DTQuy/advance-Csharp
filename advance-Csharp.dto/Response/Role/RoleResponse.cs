namespace advance_Csharp.dto.Response.Role
{
    public class RoleResponse
    {
        /// <summary>
        /// Role id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
    }
}
