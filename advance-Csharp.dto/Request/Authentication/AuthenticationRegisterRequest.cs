using System.ComponentModel.DataAnnotations;

namespace advance_Csharp.dto.Request.Authentication
{
    public class AuthenticationRegisterRequest
    {
        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// LastName
        /// </summary>
        [Required]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// ComparePassword
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string ComparePassword { get; set; } = string.Empty;
    }
}
