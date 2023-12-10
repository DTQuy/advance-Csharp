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
        public async Task<IActionResult> GetOrdersByUserId([FromQuery] Guid userId)
        {
            try
            {
                OrderRequest orderRequest = new() { UserId = userId };

                OrderListResponse orderListResponse = await _orderService.GetOrdersByUserId(orderRequest);

                return orderListResponse != null ? Ok(orderListResponse) : BadRequest("Failed to get orders for the specified user.");
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
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
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            try
            {
                UpdateOrderStatusResponse response = await _orderService.UpdateOrderStatus(request);

                return response.Success ? Ok("Order status updated successfully.") : BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// delete-order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Route("/delete-order")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderRequest request)
        {
            try
            {
                OrderResponse deletedOrder = await _orderService.DeleteOrder(request);

                return deletedOrder != null && deletedOrder.OrderId != Guid.Empty
                    ? Ok($"Order with OrderId {request.OrderId} has been successfully deleted.")
                    : BadRequest($"Order with OrderId {request.OrderId} not found for deletion.");
            }
            catch (Exception ex)
            {
                // Log errors or send errors to a logging service
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }
    }
}
