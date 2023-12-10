using advance_Csharp.dto.Request.Order;
using advance_Csharp.dto.Response.Order;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Test
{
    [TestClass]
    public class OrderServiceTest
    {
        private readonly IOrderService orderService;

        public OrderServiceTest(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// GetOrderByUserId happy case
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetOrderByUserIdTestAsync()
        {
            OrderRequest request = new();
            {
                request.UserId = new Guid("7950ca07-f4f9-4c22-921f-0c0d38c9b4a1");

            }
            OrderListResponse response = await orderService.GetOrdersByUserId(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response != null && response.Orders != null && response.Orders.Count > 0);

        }
    }

}
