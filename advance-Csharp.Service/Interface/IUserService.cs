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

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserDeleteResponse> DeleteUser(UserDeleteRequest request);

        Task<UserDeleteResponse> DeleteUser(Guid id);
    }
}
