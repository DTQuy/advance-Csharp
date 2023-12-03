namespace advance_Csharp.dto.Response.User
{
    public class UserCreateResponse : UserResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// userResponse
        /// </summary>
        public UserResponse? userResponse { get; set; }

    }
}
