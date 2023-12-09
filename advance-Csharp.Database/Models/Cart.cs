using System.ComponentModel.DataAnnotations.Schema;

namespace advance_Csharp.Database.Models
{
    /// <summary>
    /// Table Cart
    /// </summary>
    [Table("cart")]
    public class Cart : BaseEntity
    {
        /// <summary>
        /// User id in cart
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Data Cart detail
        /// </summary>
        public List<CartDetail>? CartDetails { get; set; }
    }
}
