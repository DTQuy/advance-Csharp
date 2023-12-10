using advance_Csharp.dto.Request.Cart;
using advance_Csharp.dto.Response.Cart;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Test
{
    [TestClass]
    public class CartServiceTest
    {
        private readonly ICartService cartService;

        public CartServiceTest(ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// GetApplicationCart happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationCartTestAsync()
        {
            GetAllCartRequest request = new();
            {
                request.PageSize = 1;
                request.PageIndex = 10;
            }
            GetAllCartResponse response = await cartService.GetAllCarts(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response?.Carts?.Count > 0);
        }

        /// <summary>
        /// GetCartById Happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetCartByIdTestAsync()
        {
            GetCartByUserIdRequest request = new();
            {
                request.UserId = new Guid("7950ca07-f4f9-4c22-921f-0c0d38c9b4a1");

            }
            CartResponse response = await cartService.GetCartByUserId(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response != null && response.CartDetails != null && response.CartDetails.Count > 0);

        }
    }
}
