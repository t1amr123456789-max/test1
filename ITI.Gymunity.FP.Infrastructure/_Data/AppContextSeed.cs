using ITI.Gymunity.FP.Domain.Models.Enums;
using ITI.Gymunity.FP.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Infrastructure._Data
{
    public class AppContextSeed
    {
        public static void SeedDatabase(ModelBuilder modelBuilder)
        {

        }

        public static async Task SeedIdentityDataAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed roles first
            await SeedRolesAsync(roleManager);

            // Seed users
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { UserRole.Admin, UserRole.Trainer, UserRole.Client };

            foreach (var role in roles)
            {
                var roleString = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleString))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleString));
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"✓ Created role: {roleString}");
                    }
                    else
                    {
                        Console.WriteLine($"✗ Failed to create role {roleString}: {string.Join("; ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            var usersToSeed = new[]
            {
                new { FullName = "Admin User", UserName = "admin", Email = "admin@Gymunity.com", Password = "Admin@123", Role = UserRole.Admin },
                new { FullName = "Default Admin", UserName = "adminn", Email = "admin@test.com", Password = "Password@123", Role = UserRole.Admin },
                new { FullName = "Default Trainer", UserName = "trainer", Email = "trainer@test.com", Password = "Password@123", Role = UserRole.Trainer },
                new { FullName = "Default Client", UserName = "client", Email = "client@test.com", Password = "Password@123", Role = UserRole.Client }
            };

            foreach (var userData in usersToSeed)
            {
                var existingUser = await userManager.FindByEmailAsync(userData.Email);

                if (existingUser == null)
                {
                    var user = new AppUser()
                    {
                        FullName = userData.FullName,
                        UserName = userData.UserName,
                        Email = userData.Email,
                        Role = userData.Role,
                        EmailConfirmed = true // Important: confirm email for seeded users
                    };

                    var result = await userManager.CreateAsync(user, userData.Password);

                    if (result.Succeeded)
                    {
                        var roleResult = await userManager.AddToRoleAsync(user, userData.Role.ToString());
                        if (roleResult.Succeeded)
                        {
                            Console.WriteLine($"✓ Created user: {userData.Email} with role {userData.Role}");
                        }
                        else
                        {
                            Console.WriteLine($"✗ Created user {userData.Email} but failed to assign role: {string.Join("; ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"✗ Failed to create user {userData.Email}: {string.Join("; ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    // User exists - verify role assignment
                    var roles = await userManager.GetRolesAsync(existingUser);
                    if (!roles.Contains(userData.Role.ToString()))
                    {
                        await userManager.AddToRoleAsync(existingUser, userData.Role.ToString());
                        Console.WriteLine($"⚠ Updated role for existing user: {userData.Email}");
                    }
                    else
                    {
                        Console.WriteLine($"⊙ User already exists: {userData.Email}");
                    }
                }
            }
        }
    }
}