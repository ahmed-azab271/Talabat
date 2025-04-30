using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entites.Identity;

namespace TalabatAPIs.Exctensions
{
    public static class GetUserWithAddressExtension
    {
        public static async Task<AppUser> FindUserIncludedWithAddressAsync (this UserManager<AppUser> userManger , ClaimsPrincipal User )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var ReturndUser = await userManger.Users.Include(x => x.Address).FirstOrDefaultAsync(E=>E.Email == email);
            return ReturndUser;
        }
    }
}
