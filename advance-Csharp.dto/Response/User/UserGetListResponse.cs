namespace advance_Csharp.dto.Response.User
{
    /// <summary>
    /// User GetList Response
    /// </summary>
    public class UserGetListResponse
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Data return
        /// </summary>
        public List<UserResponse> Data { get; set; } = new List<UserResponse>();
    }
}
