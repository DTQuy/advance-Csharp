using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.dto.Response.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<ProductGetListResponse> GetApplicationProductList(ProductGetListRequest request);
    }
}
