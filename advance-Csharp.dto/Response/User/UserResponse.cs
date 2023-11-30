﻿namespace advance_Csharp.dto.Response.User
{
    /// <summary>
    /// User Response
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Created At
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Created At UtcNow
        /// </summary>
        public UserResponse()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
