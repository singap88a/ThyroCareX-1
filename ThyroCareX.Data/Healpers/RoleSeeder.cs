using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Data.Healpers
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            //Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));

            if (!await roleManager.RoleExistsAsync("Doctor")) 
                await roleManager.CreateAsync(new IdentityRole<int>("Doctor"));

            if (!await roleManager.RoleExistsAsync("Patient"))
                await roleManager.CreateAsync(new IdentityRole<int>("Patient"));

            //Seed Admin User
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@thyrocare.com",
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                Gender = Enums.Gender.Male, 
                Address = "Admin Address",
                City = "Cairo",
                ZipCode = "12345",
               
            };

            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createResult = await userManager.CreateAsync(adminUser, "Admin123$");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
