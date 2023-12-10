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
        private readonly DbContextOptions<AdvanceCsharpContext> dbContextOptions;

        public ApplicationService(DbContextOptions<AdvanceCsharpContext> dbContextOptions)
        {

            this.dbContextOptions = dbContextOptions;
        }

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
            using (AdvanceCsharpContext context = new(dbContextOptions))
            {
                if (context.AppVersions != null)
                {
                    IQueryable<AppVersion> query = context.AppVersions
                    .Where(a => a.Version.Contains(request.Version))
                    .OrderBy(a => a.Version)
                    .AsQueryable(); // not excute
                    // Debug linq
                    string queryString = query
                        .Skip(request.PageSize * (request.PageIndex - 1))
                        .Take(request.PageSize).ToQueryString();
                    Console.WriteLine(queryString);
                    appVersionGetListResponse.Data = await query
                        .Skip(request.PageSize * (request.PageIndex - 1))
                        .Take(request.PageSize)
                        .Select(a => new AppVersionResponse
                        {
                            Id = a.Id,
                            Version = a.Version
                        }).ToListAsync();
                    _ = await query.CountAsync();
                };
            }
            return appVersionGetListResponse;
        }
    }
}