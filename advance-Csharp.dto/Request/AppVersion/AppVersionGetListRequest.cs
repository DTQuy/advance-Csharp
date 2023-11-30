using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Request.AppVersion
{
    /// <summary>
    /// App version get list
    /// </summary>
    public class AppVersionGetListRequest : IPagingRequest
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Search by Version
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
}
