namespace advance_Csharp.dto.Response.User
{
    public class UserGenerateTokenResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Succses
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
