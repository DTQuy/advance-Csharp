using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.AppVersion
{
    public class AppVersionResponse
    {
        public Guid Id { get; set; }
        public string Version { get; set; } = string.Empty;
    }
}
