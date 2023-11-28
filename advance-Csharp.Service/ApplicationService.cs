using advance_Csharp.dto.Request.AppVersion;
using advance_Csharp.dto.Request.Product;
using advance_Csharp.dto.Response.AppVersion;
using advance_Csharp.dto.Response.Product;
using advance_Csharp.Service.Interface;
using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service
{
    public class ApplicationService : IApplicationService
    {
        public async Task<AppVersionGetListResponse> GetApplicationVersionList(AppVersionGetListRequest request)
        {
            AppVersionGetListResponse appVersionGetListResponse = new AppVersionGetListResponse()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };
            using (AdvanceCsharpContext context = new AdvanceCsharpContext())
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
        public async Task<ProductGetListResponse> GetApplicationProductList(ProductGetListRequest request)
        {
            ProductGetListResponse productGetListResponse = new ProductGetListResponse()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };
            using (AdvanceCsharpContext context = new AdvanceCsharpContext())
            {
                IQueryable<Product> query = context.Products.Where(a => a.Category.Contains(request.Category)); // not excute
                productGetListResponse.Data = await query.Select(a => new ProductResponse
                {
                    Id = a.Id,
                    Category = a.Category
                }).ToListAsync();
            }
            return productGetListResponse;

        }
    }
}