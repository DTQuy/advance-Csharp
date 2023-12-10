using advance_Csharp.dto.Request.User;
using advance_Csharp.dto.Response.User;
using advance_Csharp.Service.Interface;

namespace advance_Csharp.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService userService;

        public UserServiceTest(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// GetApplicationUser
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationUserListTestAsync()
        {
            UserGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Email = string.Empty,
                PhoneNumber = string.Empty,

            };
            UserGetListResponse response = await userService.GetApplicationUserList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }

        /// <summary>
        /// GetApplicationProductListWithEmail happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationUserListWithEmailTestAsync()
        {
            UserGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Email = "dquy1514@gmail.com",
                PhoneNumber = string.Empty,

            };
            UserGetListResponse response = await userService.GetApplicationUserList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }

        /// <summary>
        /// GetApplicationUserListWithPhoneNumber happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetApplicationUserListWithPhoneNumberTestAsync()
        {
            UserGetListRequest request = new()
            {
                PageIndex = 1,
                PageSize = 10,
                Email = string.Empty,
                PhoneNumber = "0974322724",

            };
            UserGetListResponse response = await userService.GetApplicationUserList(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Data.Count > 0);
        }
    }
}
