using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Request.User
{
    public class UserSearchRequest
    {
        /// <summary>
        /// Search Email
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
