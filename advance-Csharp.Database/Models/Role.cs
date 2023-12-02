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
        /// User id
        /// </summary>
        public Guid IdUser { get; set; }

        /// <summary>
        /// role name
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
    }
}
