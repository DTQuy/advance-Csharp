using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advance_Csharp.dto.Response.Authentication
{
    public class AuthenticationLoginResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
