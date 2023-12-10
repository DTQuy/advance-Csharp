using advance_Csharp.Database;
using advance_Csharp.Database.Contants;
using advance_Csharp.Database.Models;

namespace advance_Csharp.Service.Seeding
{
    public class DbInitializer
    {
        public static async Task Initialize(AdvanceCsharpContext context)
        {
            if (context != null)
            {

                if (context.Roles != null && !context.Roles.Any())
                {

                    context.Roles.AddRange(new List<Role>
                    {
                        new() { Id = new Guid(ConstantSystem.AdminRoleId), RoleName = ConstantSystem.AdminRole},
                        new() { Id = new Guid(ConstantSystem.UserRoleId), RoleName = ConstantSystem.UserRoleId}
                    });
                }



                if (context.Users != null && context.Users.Any(t => t.Id == new Guid(ConstantSystem.AdminUserId)))
                {

                    string passAdminHash = BCrypt.Net.BCrypt.HashPassword("11223344a@");
                    string passTesterHash = BCrypt.Net.BCrypt.HashPassword("11223344@a");

                    context.Users.AddRange(new List<User>
                    {
                        new()
                        {
                            Id = new Guid(ConstantSystem.AdminRoleId),
                            FirstName = "Quy",
                            LastName="Đặng",
                            Email="dquy1514@gmail.com",
                            PhoneNumber="0974322724",
                            Password =passAdminHash

                        },
                         new()
                        {
                            Id = new Guid(ConstantSystem.TesterUserId),
                            FirstName = "Quy",
                            LastName="Test",
                            Email="quyTest@gmail.com",
                            PhoneNumber="",
                            Password =passTesterHash

                        }

                    });
                }


                if (context.UserRoles != null && !context.UserRoles.Any())
                {
                    List<UserRole> appUserRoleList = new();
                    if (!context.UserRoles.Any(t => t.UserId == new Guid(ConstantSystem.AdminUserId)))
                    {
                        appUserRoleList.Add(new UserRole { UserId = new Guid(ConstantSystem.AdminUserId), RoleId = new Guid(ConstantSystem.AdminRoleId) });
                    }

                    if (!context.UserRoles.Any(t => t.UserId == new Guid(ConstantSystem.TesterUserId)))
                    {
                        appUserRoleList.Add(new UserRole { UserId = new Guid(ConstantSystem.TesterUserId), RoleId = new Guid(ConstantSystem.UserRoleId) });

                    }
                    context.UserRoles.AddRange(appUserRoleList);
                }

                _ = await context.SaveChangesAsync();

            }
        }

    }
}
