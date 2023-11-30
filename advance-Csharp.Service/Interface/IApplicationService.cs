using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.dto.Response.User;
namespace advance_Csharp.Service.Interface
{
    public interface IApplicationService
    {
        /// <summary>
        /// Get application by version string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppVersionGetListResponse> GetApplicationVersionList(AppVersionGetListRequest request);

        /// <summary>
        /// Get application by Category string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductGetListResponse> GetApplicationProductList(ProductGetListRequest request);

        /// <summary>
        /// Get application by Email string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGetListResponse> GetApplicationUserList(UserGetListRequest request);
    }
}
