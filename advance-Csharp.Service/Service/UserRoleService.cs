using System;
using System.Threading.Tasks;
using advance_Csharp.Database;
using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.UserRole;
using advance_Csharp.dto.Response.UserRole;
using advance_Csharp.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Service.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitWork unitWork;
        private readonly string email;
        private readonly AdvanceCsharpContext context;

        public UserRoleService(IUnitWork unitWork, IHttpContextAccessor httpContextAccessor, AdvanceCsharpContext context)
        {
            this.unitWork = unitWork;
            this.email = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserRoleCreateResponse> AddUserRole(UserRoleCreateRequest request)
        {
            UserRoleCreateResponse response = new();

            try
            {
                // Ensure that context and context.UserRoles are not null
                if (context != null && context.UserRoles != null)
                {
                    UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                    {
                        RoleId = request.RoleId,
                        UserId = request.UserId
                    };

                    UserRoleGetByIdResponse userRoleGetByIdResponse = await GetUserRoleById(userRoleGetByIdRequest);

                    // Check if the role already exists for the user
                    if (userRoleGetByIdResponse != null)
                    {
                        response.Message = $"User already has the role";
                    }
                    else
                    {
                        UserRole newUserRole = new()
                        {
                            UserId = request.UserId,
                            RoleId = request.RoleId
                        };

                        // Add the role to the user
                        var entry = await context.UserRoles.AddAsync(newUserRole);

                        if (entry != null && entry.State == EntityState.Added)
                        {
                            // Entry is not null, and the entity is added successfully
                            bool addResult = await unitWork.CompleteAsync(email);

                            if (addResult)
                            {
                                // Role added successfully
                                response.Message = $"Role added to the user successfully.";
                            }
                            else
                            {
                                response.Message = "Failed to add role. Please try again later.";
                            }
                        }
                        else
                        {
                            response.Message = "Failed to add role. Please try again later.";
                        }
                    }
                }
                else
                {
                    response.Message = "Error: context or context.UserRoles is null.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                response.Message = $"An error occurred while adding user role: {ex.Message}";
            }

            return response;
        }


        public Task<UserRoleGetByIdResponse> GetUserRoleById(UserRoleGetByIdRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
