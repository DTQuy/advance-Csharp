using System.ComponentModel.DataAnnotations;

namespace advance_Csharp.dto.Request.Authentication
{
    public class AuthenticationLoginRequest
    {
        /// <summary>
        /// Email login
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
