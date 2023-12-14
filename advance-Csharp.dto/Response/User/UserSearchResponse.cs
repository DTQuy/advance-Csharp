namespace advance_Csharp.dto.Response.User
{
    public class UserSearchResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// data User Response
        /// </summary>
        public List<UserResponse> Data { get; set; } = new List<UserResponse>();
    }
}
