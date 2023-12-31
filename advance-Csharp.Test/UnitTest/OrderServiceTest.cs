﻿using advance_Csharp.dto.Request.Order;
using advance_Csharp.dto.Response.Order;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Test.UnitTest
{
    [TestClass]
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;

        public OrderServiceTest()
        {
            _orderService = DomainServiceCollectionExtensions.SetupOrderService();
        }


        /// <summary>
        /// GetApplicationUser
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationOrderListTestAsync()
        {
            GetAllOrderRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,

            };
            GetAllOrderResponse response = await _orderService.GetAllOrders(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Orders.Count > 0);
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
            OrderListResponse response = await _orderService.GetOrdersByUserId(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response != null && response.Orders != null && response.Orders.Count > 0);

        }
    }
}
