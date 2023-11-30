namespace advance_Csharp.dto.Response.Product
{
    /// <summary>
    /// Product get list response
    /// </summary>
    public class ProductGetListResponse
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
        public List<ProductResponse> Data { get; set; } = new List<ProductResponse>();
    }
}
