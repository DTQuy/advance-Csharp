namespace advance_Csharp.dto.Response.Authentication
{
    public class AuthenticationRegisterResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        public List<AuthenticationResponse> Data { get; set; } = new List<AuthenticationResponse>();
    }
}
