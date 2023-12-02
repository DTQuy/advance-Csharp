using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class UserSevice : IUserSevice
    {
        /// <summary>
        /// UserGetListResponse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserGetListResponse> GetApplicationUserList(UserGetListRequest request)
        {
            UserGetListResponse userGetListResponse = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };
            using (AdvanceCsharpContext context = new())
            {
                IQueryable<User> query = context.Users.Where(a => a.Email.Contains(request.Email)); // not excute
                userGetListResponse.Data = await query.Select(a => new UserResponse
                {
                    Id = a.Id,
                    Email = a.Email
                }).ToListAsync();
            }
            return userGetListResponse;

        }
    }
}
