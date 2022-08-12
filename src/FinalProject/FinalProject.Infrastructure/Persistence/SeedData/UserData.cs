using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Persistence.SeedData
{
    public class UserData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {

            var _userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>))!;
            var _emailStore = (IUserEmailStore<ApplicationUser>)serviceProvider.GetService(typeof(IUserEmailStore<ApplicationUser>))!;
            var _userStore = (IUserStore<ApplicationUser>)serviceProvider.GetService(typeof(IUserStore<ApplicationUser>))!;

            var user = Activator.CreateInstance<ApplicationUser>();

            if (await _userManager.FindByEmailAsync("admin@gmail.com") is null)
            {
                await _userStore.SetUserNameAsync(user, "admin@gmail.com", CancellationToken.None);
                await _emailStore.SetEmailAsync(user, "admin@gmail.com", CancellationToken.None);
                var result = await _userManager.CreateAsync(user, "Asdasdasd");

                if (result.Succeeded)
                {
                    // Add related data to claims
                    var adminClaim = new Claim("IsAdmin", true.ToString());
                    await _userManager.AddClaimAsync(user, adminClaim);

                    var nameClaim = new Claim(ClaimTypes.Name, "Admin");
                    await _userManager.AddClaimAsync(user, nameClaim);
                }
            }


        }
    }
}
