using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Role;
using advance_Csharp.dto.Response.Role;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class RoleService : IRoleService
    {
        private readonly AdvanceCsharpContext _context;

        /// <summary>
        /// RoleService
        /// </summary>
        /// <param name="context"></param>
        public RoleService(AdvanceCsharpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Search Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RoleSearchResponse> SearchRole(RoleSearchRequest request)
        {
            RoleSearchResponse roleSearchResponse = new();
            try
            {
                IQueryable<Role> query = _context.Roles != null ? _context.Roles.AsQueryable() : Enumerable.Empty<Role>().AsQueryable();

                if (!string.IsNullOrEmpty(request.RoleName))
                {
                    query = query.Where(a => a.RoleName.Contains(request.RoleName));
                }

                roleSearchResponse.Data = await query.Select(a => new RoleResponse
                {
                    RoleName = a.RoleName,
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                roleSearchResponse.Message = $"An error occurred while searching for roles: {ex.Message}";
            }

            return roleSearchResponse;
        }
    }

}
