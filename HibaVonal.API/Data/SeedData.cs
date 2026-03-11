using HibaVonal.API.Models;
using Microsoft.AspNetCore.Identity;

namespace HibaVonal.API.Data
{
    public static class SeedData
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = { "Student", "Maintenance", "MaintenanceManager", "Admin", "DEV" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
            }

            await CreateUserWithRole(userManager, "admin@hibavonal.hu", "HV_Admin#2026!ax", "Admin");
            await CreateUserWithRole(userManager, "manager@hibavonal.hu", "Maint_Mgr$77_K", "MaintenanceManager");
            await CreateUserWithRole(userManager, "szaki@hibavonal.hu", "Tech_Support*99", "Maintenance");
            await CreateUserWithRole(userManager, "pisti@egyetem.hu", "Stud_Uni@2026x", "Student");
            await CreateUserWithRole(userManager, "dev@hibavonal.hu", "Dev_Root_Access%1", "DEV");
        }

        private static async Task CreateUserWithRole(UserManager<AppUser> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
