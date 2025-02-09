using Microsoft.AspNetCore.Identity;

namespace SmartParkingSystem.Seeder
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Administrator", "Owner", "Driver" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new IdentityRole
                    {
                        Name = roleName,
                    };

                    try
                    {
                        var result = await roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            Console.WriteLine($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception creating role {roleName}: {ex.Message}");
                    }
                }
            }
        }


        public class CustomRoleValidator<TRole> : IRoleValidator<TRole> where TRole : IdentityRole
        {
            public Task<IdentityResult> ValidateAsync(RoleManager<TRole> manager, TRole role)
            {
                // Bypass validation by returning success
                return Task.FromResult(IdentityResult.Success);
            }
        }


    }
}
