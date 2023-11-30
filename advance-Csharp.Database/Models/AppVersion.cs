using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    /// <summary>
    /// Table AppVersion
    /// </summary>
    [Table("appVersion")]
    public class AppVersion : BaseEntity
    {
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
}
