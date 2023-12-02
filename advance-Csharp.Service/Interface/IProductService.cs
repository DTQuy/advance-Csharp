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
        Task<ProductDeleteResponse> DeleteProduct(Guid id);
    }
}
