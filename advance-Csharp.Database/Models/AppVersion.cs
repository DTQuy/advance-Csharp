using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.Database.Models
{
    [Table("appVersion")]
    public class AppVersion : BaseEntity
    {
        public string Version { get; set; } = string.Empty;
    }
}
