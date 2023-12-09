namespace advance_Csharp.dto.Response.Authentication
{
    public class AuthenticationResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public DateTimeOffset? CreatedAt { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
