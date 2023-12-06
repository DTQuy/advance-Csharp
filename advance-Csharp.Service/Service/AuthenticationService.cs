using advance_Csharp.Database.Models;
using advance_Csharp.dto.Request.Authentication;
using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.Authentication;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService? userService;
        private readonly IUserRoleService? userRoleService;
        private readonly IRoleService? roleService;
        private readonly IJwtService? jwtUtils;

        public AuthenticationService(IUserService userService, IJwtService jwtUtils, IUserRoleService userRoleService, IRoleService roleService)
        {


            this.userService = userService;
            this.jwtUtils = jwtUtils;
            this.userRoleService = userRoleService;
            this.roleService = roleService;

        }

        public async Task<AuthenticationLoginResponse> AccountAuthentication(AuthenticationLoginRequest Request)
        {
            AuthenticationLoginResponse response = new AuthenticationLoginResponse();

            try
            {
                // Check if userService and jwtUtils are not null
                if (userService == null || jwtUtils == null)
                {
                    response.Message = "Authentication service or JWT utility is not available.";
                    return response;
                }

                // Attempt to find the user by email
                UserSearchRequest userSearchRequest = new UserSearchRequest
                {
                    Email = Request.Email,
                };

                UserSearchResponse existedUser = await userService.SearchUser(userSearchRequest);

                if (existedUser.Data.Any())
                {
                    var foundUser = existedUser.Data.First();

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
        }

    }
}
