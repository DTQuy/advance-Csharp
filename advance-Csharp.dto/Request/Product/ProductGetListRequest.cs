﻿namespace advance_Csharp.dto.Request.Product
{
    public class ProductGetListRequest : IPagingRequest
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Search: Category
        /// </summary>
        public string Category { get; set; } = string.Empty;


    }
}
