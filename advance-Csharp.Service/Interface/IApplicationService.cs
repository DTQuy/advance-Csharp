using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Response.AppVersion;
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
    }
}
