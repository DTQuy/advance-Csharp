using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Authorization;
using advance_Csharp.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace advance_Csharp.Service.Service
{
    public class JwtService : IJwtService
    {
        private readonly AppSetting appSetting;

        public JwtService(IOptions<AppSetting> appSettings)
        {
            appSetting = appSettings.Value;
        }

        /// <summary>
        /// Generate an access token for the provided userResponse.
        /// </summary>
        /// <param name="userResponse">The user response containing user information.</param>
        /// <returns>A task representing the asynchronous operation that yields the generated access token.</returns>
        public async Task<string> GenerateAccessToken(UserResponse userResponse)
        {
            return await Task.Run(() =>
            {
                JwtSecurityTokenHandler tokenHandler = new();

                byte[] key = Encoding.ASCII.GetBytes(appSetting.Secret);

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", userResponse.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            });
        }

        /// <summary>
        /// Validate the provided access token and return the user ID.
        /// </summary>
        /// <param name="accessToken">The access token to validate.</param>
        /// <returns>The user ID if the validation is successful; otherwise, null.</returns>
        public Guid? ValidateAccessToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new();

            byte[] key = Encoding.ASCII.GetBytes(appSetting.Secret);

            try
            {
                _ = tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
