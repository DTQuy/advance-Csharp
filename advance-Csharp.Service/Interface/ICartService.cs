using advance_Csharp.dto.Request.Cart;
using advance_Csharp.dto.Response.Cart;

namespace advance_Csharp.Service.Interface
{
    public interface ICartService
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<List<CartResponse>> GetAllCarts();

        /// <summary>
        /// Get Cart By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CartResponse> GetCartByUserId(Guid userId);

        /// <summary>
        /// AddProductToCart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddProductToCart(CartRequest request);

        /// <summary>
        /// GetProductPriceByIdAsync
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        /// <summary>
        /// ViewCart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CartResponse> ViewCart(Guid userId);

        /// <summary>
        /// DeleteProductFromCart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<bool> DeleteProductFromCart(Guid userId, Guid productId);

        /// <summary>
        /// UpdateQuantity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<bool> UpdateQuantity(Guid userId, Guid productId, int quantity);
    }

}
