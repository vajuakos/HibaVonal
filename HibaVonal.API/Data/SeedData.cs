using HibaVonal.API.Models;
using Microsoft.AspNetCore.Identity;

namespace HibaVonal.API.Data
{
    public static class SeedData
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("admin@hibavonal.hu");

            if (user == null)
            {
                var admin = new AppUser
                {
                    Email = "admin@hibavonal.hu",
                    UserName = "admin@hibavonal.hu",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "NagyonTitkosJelszo123!");
            }
        }
    }
}
