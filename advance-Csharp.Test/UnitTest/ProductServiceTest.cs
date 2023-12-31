using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Test.UnitTest
{
    [TestClass]
    public class ProductServiceTest
    {
        private readonly IProductService _productService;

        /// <summary>
        /// ProductServiceTest
        /// </summary>
        public ProductServiceTest()
        {
            _productService = DomainServiceCollectionExtensions.SetupProductService();
        }

        /// <summary>
        /// GetApplicationProductList happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationProductListTestAsync()
        {
            ProductGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Name = string.Empty,
                Category = string.Empty,
                PriceFrom = string.Empty,
                PriceTo = string.Empty,
            };
            ProductGetListResponse response = await _productService.GetApplicationProductList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }

        /// <summary>
        /// GetApplicationProductListWithName happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationProductListWithNameTestAsync()
        {
            ProductGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Name = "Product 40183",
                Category = string.Empty,
                PriceFrom = string.Empty,
                PriceTo = string.Empty,
            };
            ProductGetListResponse response = await _productService.GetApplicationProductList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }

        /// <summary>
        /// GetApplicationProductListWithCategory happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationProductListWithCategoryTestAsync()
        {
            ProductGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Name = string.Empty,
                Category = "Cloth",
                PriceFrom = string.Empty,
                PriceTo = string.Empty,
            };
            ProductGetListResponse response = await _productService.GetApplicationProductList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }

        /// <summary>
        /// GetApplicationProductListWithPriceFromTo happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationProductListWithPriceFromToTestAsync()
        {
            ProductGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Name = string.Empty,
                Category = string.Empty,
                PriceFrom = "1000000",
                PriceTo = "1100000",
            };
            ProductGetListResponse response = await _productService.GetApplicationProductList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }
    }
}