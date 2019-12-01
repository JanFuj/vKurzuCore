using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using vKurzuCore.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace vKurzuCore.Models
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string adminUserName, string adminPw)
        {
            string[] roles = new string[] { Helpers.Constants.Roles.Admin, Helpers.Constants.Roles.Lector, Helpers.Constants.Roles.User };

            using (var context = new vKurzuDbContext(serviceProvider.GetRequiredService<DbContextOptions<vKurzuDbContext>>()))
            {
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
                foreach (string role in roles)
                {
                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                var adminID = await EnsureUser(serviceProvider, adminUserName, adminPw);
            }

        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                           string UserName, string testUserPw)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    Email = UserName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = await userManager.CreateAsync(user, testUserPw);
                if (userResult.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, Helpers.Constants.Roles.Admin);
                    if (result.Succeeded)
                    {

                    }
                }
            }


            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
