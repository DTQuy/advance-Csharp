using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;

namespace advance_Csharp.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Get application by Email and PhoneNumber string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGetListResponse> GetApplicationUserList(UserGetListRequest request);

        /// <summary>
        /// GetUserById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGetByIdResponse> GetUserById(UserGetByIdRequest request);

        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserCreateResponse> CreateUser(UserCreateRequest request);

        /// <summary>
        /// UpdateUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserUpdateResponse> UpdateUser(UserUpdateRequest request);

        Task<UserDeleteResponse> DeleteUser(UserDeleteRequest request);

        /*/// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGenerateTokenResponse> GenerateToken(UserGenerateTokenRequest request);*/

        /// <summary>
        /// SearchUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserSearchResponse> SearchUser(UserSearchRequest request);
    }
}
