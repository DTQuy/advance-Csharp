﻿namespace advance_Csharp.dto.Response.Role
{
    public class RoleSearchResponse
    {
        /// <summary>
        /// Massage
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// role Response
        /// </summary>
        public Guid RoleId { get; set; }
        public List<RoleResponse> Data { get; set; } = new List<RoleResponse>();
    }
}
