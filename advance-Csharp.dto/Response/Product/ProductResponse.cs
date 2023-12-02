﻿using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.dto.Response.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price
        /// </summary>
        public string Price { get; set; } = string.Empty;

        /// <summary>
        /// Quantity
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; } = "VND";

        /// <summary>
        /// Images
        /// </summary>
        public string Images { get; set; } = string.Empty;

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Created at
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// ProductResponse: CreatedAt = DateTimeOffset.UtcNow;
        /// </summary>
        public ProductResponse() 
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        /*// <summary>
       /// Parse Price from string to decimal
       /// </summary>
       [NotMapped]
       public decimal PriceDecimal
       {
           get => decimal.Parse(Price);
           set => Price = value.ToString();
       }*/
    }
}
