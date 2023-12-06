namespace advance_Csharp.dto.Request.User
{
    public class UserGenerateTokenRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string? Token { get; set; }
    }
}
