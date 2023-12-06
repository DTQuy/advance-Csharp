using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    [Table("userRole")]
    public class UserRole
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
