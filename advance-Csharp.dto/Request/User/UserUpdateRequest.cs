namespace advance_Csharp.dto.Request.User
{
    public class UserUpdateRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// IsPhoneValid
        /// </summary>
        public bool IsPhoneValid => decimal.TryParse(PhoneNumber, out _);
    }
}
