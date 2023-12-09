using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Order;
using advance_Csharp.dto.Response.Order;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly DbContextOptions<AdvanceCsharpContext> _context;
        public OrderService(DbContextOptions<AdvanceCsharpContext> dbContextOptions)
        {
            _context = dbContextOptions;
        }

        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderResponse> CreateOrder(OrderRequest request)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                // Fetch cart details based on the provided userId
                List<CartDetail> cartDetails = await FetchCartDetails(request.UserId);

                // Calculate the total amount from fetched cart details
                decimal totalAmount = CalculateTotalAmount(cartDetails);

                // Create an order
                Order order = new()
                {
                    UserId = request.UserId,
                    OrderDate = DateTimeOffset.UtcNow,
                    TotalAmount = totalAmount,
                    OrderDetails = cartDetails?.Select(cartDetail => new OrderDetail
                    {
                        ProductId = cartDetail.ProductId,
                        Price = decimal.TryParse(cartDetail.Price, out decimal price) ? price : 0,
                        Quantity = cartDetail.Quantity,
                        OrderStatus = false // Default order status is false
                    }).ToList() ?? new List<OrderDetail>()
                };

                // Save the order to the database
                _ = context.Orders?.Add(order);
                _ = await context.SaveChangesAsync();

                // Create OrderResponse
                OrderResponse orderResponse = new()
                {
                    OrderId = order.Id,
                    UserId = order.UserId,
                    TotalAmount = order.TotalAmount,
                    OrderDetails = order.OrderDetails?.Select(od => new OrderDetailResponse
                    {
                        Id = od.Id.GetValueOrDefault(),
                        ProductId = od.ProductId,
                        Price = od.Price,
                        Quantity = od.Quantity,
                        OrderStatus = od.OrderStatus // Include OrderStatus in the response
                    }).ToList() ?? new List<OrderDetailResponse>(),
                    // Add other information if needed
                };

                return orderResponse;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine($"An error occurred while creating the order: {ex.Message}");
                return new OrderResponse
                {
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Get orders by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OrderListResponse> GetOrdersByUserId(Guid userId)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                // Check if context.Orders is not null
                if (context.Orders != null)
                {
                    // Retrieve the orders for the given user ID
                    List<OrderResponse> orderResponses = await context.Orders
                        .Include(o => o.OrderDetails)
                        .Where(o => o.UserId == userId)
                        .Select(o => new OrderResponse
                        {
                            Message = "Order information of UserId" + userId,
                            OrderId = o.Id,
                            UserId = o.UserId,
                            OrderDate = o.OrderDate.HasValue ? o.OrderDate.Value
                                            .ToOffset(new TimeSpan(7, 0, 0)) : DateTimeOffset.MinValue,
                            TotalAmount = o.TotalAmount,
                            OrderDetails = (o.OrderDetails != null) ? o.OrderDetails.Select(od => new OrderDetailResponse
                            {
                                Id = od.Id.GetValueOrDefault(),
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Quantity = od.Quantity,
                                OrderStatus = od.OrderStatus,
                                OrderStatusDescription = od.OrderStatus ? "Đã thanh toán" : "Chưa thanh toán"
                                // Add other properties as needed
                            }).ToList() : new List<OrderDetailResponse>()
                        })
                        .ToListAsync();

                    // Create the OrderListResponse
                    OrderListResponse orderListResponse = new()
                    {
                        Orders = orderResponses
                    };

                    return orderListResponse;
                }
                else
                {
                    // Handle the case where context.Orders is null
                    Console.WriteLine("Error: context.Orders is null");

                    // Return an empty OrderListResponse or take other appropriate actions
                    return new OrderListResponse { Orders = new List<OrderResponse>() };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine($"An error occurred while getting orders: {ex.Message}");

                // Return an empty OrderListResponse or take other appropriate actions
                return new OrderListResponse { Orders = new List<OrderResponse>() };
            }
        }

        /// <summary>
        /// Update Order Status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderStatus(Guid userId, bool newStatus)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);
                Order? order = null;

                if (context.Orders != null)
                {
                    order = await context.Orders
                        .Where(o => o.Id == userId)
                        .Include(o => o.OrderDetails)
                        .FirstOrDefaultAsync();
                }
                else
                {
                    // Handle the case where context.Orders is null
                    Console.WriteLine("Error: context.Orders is null");
                }

                if (order != null)
                {
                    // Update the order status
                    order.OrderDetails?.ForEach(od => od.OrderStatus = newStatus);

                    // Save changes to the database
                    _ = await context.SaveChangesAsync();

                    // If the order status is updated to true, reduce product quantities
                    if (newStatus)
                    {
                        using AdvanceCsharpContext productContext = new(_context);
                        if (order != null && order.OrderDetails != null && productContext.Products != null)
                        {
                            foreach (OrderDetail orderDetail in order.OrderDetails)
                            {
                                Product? product = await productContext.Products.FindAsync(orderDetail.ProductId);
                                if (product != null)
                                {
                                    // Ensure the product quantity is not negative
                                    product.Quantity = Math.Max(0, product.Quantity - orderDetail.Quantity);
                                }
                            }

                            _ = await productContext.SaveChangesAsync();
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine($"An error occurred while updating the order status: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// DeleteOrder by Order Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderResponse> DeleteOrder(Guid orderId)
        {
            try
            {
                using AdvanceCsharpContext context = new(_context);

                // Check if context.Orders is not null before querying
                if (context.Orders != null)
                {
                    // Retrieve the order to be deleted
                    Order? orderToDelete = await context.Orders
                        .Where(o => o.Id == orderId)
                        .Include(o => o.OrderDetails)
                        .FirstOrDefaultAsync();

                    // Check if the order exists
                    if (orderToDelete != null)
                    {
                        // Create an OrderResponse object with the order information before deletion
                        OrderResponse deletedOrderResponse = new()
                        {
                            OrderId = orderToDelete.Id,
                            UserId = orderToDelete.UserId,
                            TotalAmount = orderToDelete.TotalAmount,
                            OrderDetails = orderToDelete.OrderDetails?.Select(od => new OrderDetailResponse
                            {
                                Id = od.Id.GetValueOrDefault(),
                                ProductId = od.ProductId,
                                Price = od.Price,
                                Quantity = od.Quantity,
                                OrderStatus = od.OrderStatus,
                                OrderStatusDescription = od.OrderStatus ? "Đã thanh toán" : "Chưa thanh toán"
                            }).ToList() ?? new List<OrderDetailResponse>()
                        };

                        // Remove the order from the context
                        _ = context.Orders.Remove(orderToDelete);

                        // Save changes to the database
                        _ = await context.SaveChangesAsync();

                        // Display a message indicating successful deletion
                        Console.WriteLine($"Order with OrderId {orderId} has been successfully deleted.");

                        // Return the deleted order information
                        return deletedOrderResponse;
                    }
                    else
                    {
                        // Display a message indicating that the order was not found
                        Console.WriteLine($"Order with OrderId {orderId} not found for deletion.");

                        // You can decide what to return in case of not finding the order.
                        // For now, returning an empty OrderResponse.
                        return new OrderResponse();
                    }
                }
                else
                {
                    // Handle the case where context.Orders is null
                    Console.WriteLine("Error: context.Orders is null");

                    // You can decide what to return in case of a null context.Orders.
                    // For now, returning an empty OrderResponse.
                    return new OrderResponse();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine($"An error occurred while deleting the order: {ex.Message}");

                // You can decide what to return in case of an error.
                // For now, returning an empty OrderResponse.
                return new OrderResponse();
            }
        }

        /// <summary>
        /// CalculateTotalAmount
        /// </summary>
        /// <param name="cartDetails"></param>
        /// <returns></returns>
        private static decimal CalculateTotalAmount(List<CartDetail>? cartDetails)
        {
            // Check for null before calculating the total amount
            if (cartDetails == null || !cartDetails.Any())
            {
                return 0;
            }

            // Calculate the total amount from cart details
            return cartDetails.Sum(cartDetail => decimal.TryParse(cartDetail.Price, out decimal price) ? price * cartDetail.Quantity : 0);
        }

        /// <summary>
        /// FetchCartDetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<List<CartDetail>> FetchCartDetails(Guid userId)
        {
            using AdvanceCsharpContext context = new(_context);
            DbSet<CartDetail>? cartDetailsQuery = context.CartDetails;

            if (cartDetailsQuery != null)
            {
                return await cartDetailsQuery
                    .Where(cd => cd.Cart != null && cd.Cart.UserId == userId)
                    .ToListAsync();
            }
            else
            {
                // Handle the case where context.CartDetails is null
                Console.WriteLine("Error: context.CartDetails is null");
                return new List<CartDetail>();
            }
        }
    }
}
