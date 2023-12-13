namespace advance_Csharp.dto.Request.Product
{
    /// <summary>
    /// Product update request
    /// </summary>
    public class ProductUpdateRequest
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public string? Price { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string? Unit { get; set; } = "VND";

        /// <summary>
        /// Images
        /// </summary>
        public string? Images { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Check Price is Number
        /// </summary>
        public bool IsPriceValid => decimal.TryParse(Price, out _);
    }
}
