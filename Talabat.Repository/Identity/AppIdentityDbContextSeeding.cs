using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeeding
    {
        public static async Task SeedUserAsync (UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Ahmed Azab" ,
                    Email = "Kamelahmed271@gmail.com",
                    UserName = "Kamelahmed271",
                    PhoneNumber = "01151818529" ,
                };
                await userManager.CreateAsync(User , "Azab1");
            }
        }
    }
}
