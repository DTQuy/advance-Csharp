using advance_Csharp.dto.Request.Order;
using advance_Csharp.dto.Response.Order;

namespace advance_Csharp.Service.Interface
{
    public interface IOrderService
    {
        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderResponse> CreateOrder(OrderRequest request);

        /// <summary>
        /// Get rders by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OrderListResponse> GetOrdersByUserId(Guid userId);

        /// <summary>
        /// Update Order Status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderStatus(Guid userId, bool newStatus);

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<OrderResponse> DeleteOrder(Guid orderId);
    }
}
