using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entites.Identity;
using Talabat.Core.IServices;
using Talabat.Repository.Identity;
using Talabat.Services;

namespace TalabatAPIs.Exctensions
{
    public static class IdentityExctension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services ,IConfiguration configuration)
        {
            Services.AddScoped(typeof(ITokenService), typeof(TokenService));

            Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();


            // ( Services.AddAuthentication ) for alllowind Dependancy Inhection to UserManger / SigninManger / RoleManger
            // The rest of code for Token 
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });


            return Services;
        }
    }
}
