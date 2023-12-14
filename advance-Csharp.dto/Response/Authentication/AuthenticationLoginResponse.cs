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
        public bool Success { get; set; }
    }
}
