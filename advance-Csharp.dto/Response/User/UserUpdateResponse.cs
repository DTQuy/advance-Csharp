using advance_Csharp.dto.Response.Product;

namespace advance_Csharp.dto.Response.User
{
    public class UserUpdateResponse
    {
        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Old User
        /// </summary>
        public UserResponse? OldUser { get; set; }

        /// <summary>
        /// Updated User
        /// </summary>
        public UserResponse? UpdatedUser { get; set; }
    }
}
