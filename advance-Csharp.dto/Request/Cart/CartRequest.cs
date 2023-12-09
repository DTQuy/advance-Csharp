namespace advance_Csharp.dto.Request.Cart
{
    public class CartRequest
    {
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }

        public List<CartDetailRequest> CartDetails { get; set; } = new List<CartDetailRequest>();
    }
}
