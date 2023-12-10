using advance_Csharp.Database.Contants;
using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Request.Role;
using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Request.UserRole;
using advance_Csharp.dto.Response.Authentication;
using advance_Csharp.dto.Response.Role;
using advance_Csharp.dto.Response.User;
using advance_Csharp.dto.Response.UserRole;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IRoleService roleService;

        /// <summary>
        /// Authentication Service
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="jwtUtils"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AuthenticationService(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            this.userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(roleService));

        }
        /// <summary>
        /// RegisterUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationRegisterResponse> RegisterUser(AuthenticationRegisterRequest request)
        {
            AuthenticationRegisterResponse response = new();

            try
            {
                // Check if the user account already exists
                UserSearchRequest userSearchRequest = new()
                {
                    Email = request.Email
                };

                UserSearchResponse userSearchResponse = await userService.SearchUser(userSearchRequest);

                if (userSearchResponse?.Data?.Any() == true)
                {
                    response.Message = "An account with this email already exists.";
                    return response;
                }

                // Check if password and confirmation password match
                if (request.Password != request.ComparePassword)
                {
                    response.Message = "Password and Confirmation Password must match.";
                    return response;
                }

                // Create a new user instance
                UserCreateRequest userCreateRequest = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                UserCreateResponse userCreateResponse = await userService.CreateUser(userCreateRequest);

                if (userCreateResponse == null || userCreateResponse.UserResponse == null)
                {
                    response.Message = "Registration failed. Please check provided information.";
                    return response;
                }

                // Find the "UserRole" role
                RoleSearchRequest roleSearchRequest = new()
                {
                    RoleName = ConstantSystem.UserRole
                };

                RoleSearchResponse roleSearchResponse = await roleService.SearchRole(roleSearchRequest);

                if (roleSearchResponse is null or null)
                {
                    response.Message = "The 'UserRole' role was not found.";
                    return response;
                }

                // Check if the user already has a role
                UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                {
                    RoleId = roleSearchResponse.RoleId,
                    UserId = userCreateResponse.UserResponse.Id
                };

                UserRoleGetByIdResponse userRoleGetByIdResponse = await userRoleService.GetUserRoleById(userRoleGetByIdRequest);

                if (userRoleGetByIdResponse == null)
                {
                    // Add the role to the user
                    UserRoleCreateRequest userRoleCreateRequest = new()
                    {
                        RoleId = roleSearchResponse.RoleId,
                        UserId = userCreateResponse.UserResponse.Id
                    };

                    UserRoleCreateResponse userRoleCreateResponse = await userRoleService.AddUserRole(userRoleCreateRequest);

                    response.Message = userRoleCreateResponse != null ? "Registration Successful" : "Failed to add user to role";
                }
                else
                {
                    response.Message = "User already has a role.";
                }
            }
            catch (Exception)
            {
                response.Message = "An error occurred during registration.";
            }

            return response;
        }

        /*/// <summary>
        /// AccountAuthentication
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public async Task<AuthenticationLoginResponse> AccountAuthentication(AuthenticationLoginRequest Request)
        {
            AuthenticationLoginResponse response = new();

            try
            {
                // Check if userService and jwtUtils are not null
                if (userService == null || jwtUtils == null)
                {
                    response.Message = "Authentication service or JWT utility is not available.";
                    return response;
                }

                // Attempt to find the user by email
                UserSearchRequest userSearchRequest = new()
                {
                    Email = Request.Email,
                };

                UserSearchResponse existedUser = await userService.SearchUser(userSearchRequest);

                if (existedUser.Data.Any())
                {
                    UserResponse foundUser = existedUser.Data.First();

                    if (!BCrypt.Net.BCrypt.Verify(Request.Password, foundUser.Password))
                    {
                        response.Message = "Incorrect password. Please check your password.";
                    }
                    else
                    {
                        string newToken = await jwtUtils.GenerateAccessToken(foundUser);
                        response.Token = newToken;
                        UserGenerateTokenRequest userGenerateTokenRequest = new()
                        {
                            UserId = foundUser.Id,
                            Token = newToken
                        };
                        UserGenerateTokenResponse userGenerateTokenResponse = await userService.GenerateToken(userGenerateTokenRequest);

                        if (userGenerateTokenResponse != null)
                        {
                            // Authentication successful
                            response.Message = "Login successful";
                        }
                    }
                }
                else
                {
                    response.Message = "User not found. Please check your email.";
                }
            }
            catch (Exception)
            {
                // Log the exception here

                response.Message = "An error occurred during login.";
            }

            return response;
        }*/

    }
}
