using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    /// <summary>
    /// Role
    /// </summary>
    [Table("role")]
    public class Role
    {
        /// <summary>
        /// Id Role
        /// </summary>
        [Key]
        public Guid IdRole { get; set; }
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
