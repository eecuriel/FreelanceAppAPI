using Microsoft.AspNetCore.Identity;

namespace FreelanceAppAPI.Helpers
{
    public class SeedData
    {
        public static void SeedDataInit(RoleManager<IdentityRole> roleManager) {

            SeedRoles(roleManager);
                
        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager) {

            if (!roleManager.RoleExistsAsync("User").Result) {
                IdentityRole role  = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result) {
                IdentityRole role  = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }
    }
}