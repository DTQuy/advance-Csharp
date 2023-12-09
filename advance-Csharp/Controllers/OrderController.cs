using advance_Csharp.dto.Request.Order;
using advance_Csharp.dto.Response.Order;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace advance_Csharp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        /// <summary>
        /// create-order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("/create-order")]
        [HttpPost()]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            try
            {
                OrderResponse orderResponse = await _orderService.CreateOrder(request);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the order.");
            }
        }

        /// <summary>
        /// get-order-by-UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("/get-order-by-UserId")]
        [HttpGet()]
        public async Task<IActionResult> GetOrdersByUserId(Guid userId)
        {
            try
            {
                OrderListResponse orderListResponse = await _orderService.GetOrdersByUserId(userId);
                return Ok(orderListResponse);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while getting orders.");
            }
        }

        /// <summary>
        /// /update-order-status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        [Route("/update-order-status")]
        [HttpPut()]
        public async Task<IActionResult> UpdateOrderStatus(Guid userId, [FromBody] bool newStatus)
        {
            try
            {
                bool result = await _orderService.UpdateOrderStatus(userId, newStatus);
                return result ? Ok("Order status updated successfully.") : NotFound($"Order with UserId {userId} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the order status.");
            }
        }

        /// <summary>
        /// delete-order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Route("/delete-order")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            try
            {
                OrderResponse deletedOrderResponse = await _orderService.DeleteOrder(orderId);
                return deletedOrderResponse.OrderId != Guid.Empty
                    ? Ok(deletedOrderResponse)
                    : NotFound($"Order with OrderId {orderId} not found for deletion.");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }
    }
}
