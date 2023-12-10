using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    /// <summary>
    /// Role
    /// </summary>
    [Table("role")]
    public class Role : BaseEntity
    {
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

    }
}
