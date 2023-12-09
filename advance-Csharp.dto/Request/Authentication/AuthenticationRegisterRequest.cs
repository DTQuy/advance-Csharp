using System.ComponentModel.DataAnnotations;

namespace advance_Csharp.dto.Request.Authentication
{
    public class AuthenticationRegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string ComparePassword { get; set; } = string.Empty;
    }
}
