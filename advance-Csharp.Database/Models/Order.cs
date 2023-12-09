using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// user id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Total amount of the order
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// DateOffset
        /// </summary>
        public DateTimeOffset? OrderDate { get; set; }

        // Navigation property for OrderDetails
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
