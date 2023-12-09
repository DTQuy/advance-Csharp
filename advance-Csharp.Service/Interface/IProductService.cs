using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.Product;

namespace advance_Csharp.Service.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// Get Product by: Name, Category, Price
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductGetListResponse> GetApplicationProductList(ProductGetListRequest request);

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request);

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductUpdateResponse> UpdateProduct(ProductUpdateRequest request);

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductDeleteResponse> DeleteProduct(ProductDeleteRequest request);

        /// <summary>
        /// DeleteProduct by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDeleteResponse> DeleteProduct(Guid id);

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Product> GetProductById(Guid productId);

        /// <summary>
        /// GetProductPriceById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<string> GetProductPriceById(Guid productId);
    }
}
