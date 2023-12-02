using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// AppVersionGetListResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AppVersionGetListResponse> GetApplicationVersionList(AppVersionGetListRequest request)
        {
            AppVersionGetListResponse appVersionGetListResponse = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };
            using (AdvanceCsharpContext context = new())
            {
                IQueryable<AppVersion> query = context.AppVersions.Where(a => a.Version.Contains(request.Version)); // not excute
                appVersionGetListResponse.Data = await query.Select(a => new AppVersionResponse
                {
                    Id = a.Id,
                    Version = a.Version
                }).ToListAsync();
            }
            return appVersionGetListResponse;
        }
    }
}