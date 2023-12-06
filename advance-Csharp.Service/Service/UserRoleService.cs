using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.UserRole;
using advance_Csharp.dto.Response.UserRole;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Service.Service
{
    public class UserRoleService : IUserRoleService
    {
        // Inject dependencies through constructor
        private readonly IUserService userService; // Replace with your actual repository
        private readonly IUserRoleService userRoleService; // Replace with your actual repository

        public UserRoleService(IUserService userRepository, IUserRoleService roleRepository)
        {
            userService = userRepository;
            userRoleService = roleRepository;
        }

        public async Task<UserRoleCreateResponse> AddUserRole(UserRoleCreateRequest request)
        {
            UserRoleCreateResponse response = new UserRoleCreateResponse();

            try
            {
                // Assuming you have methods in your repository to add a user role
                bool roleAdded = await userService.AddUserRole(request.UserId, request.RoleId);

                if (roleAdded)
                {
                    // If the role is added successfully, you can retrieve the updated user role information
                    UserRoleCreateResponse updatedUserRole = await GetUserRoleByIdAsync(request.UserId, request.RoleId);

                    // Set the response with the updated user role information
                    response.UserResponse = updatedUserRole.UserResponse;
                    response.Message = "User role added successfully";
                }
                else
                {
                    // Handle the case where the role addition fails
                    response.Message = "Failed to add user role";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                response.Message = $"An error occurred while adding user role: {ex.Message}";
            }

            return response;
        }

        private async Task<UserRoleCreateResponse> GetUserRoleByIdAsync(Guid userId, Guid roleId)
        {
            // Assuming you have methods in your repository to get user role information by user ID and role ID
            UserRoleCreateResponse userRoleResponse = await _userRepository.GetUserRoleById(userId, roleId);
            return userRoleResponse;
        }
    }
}
