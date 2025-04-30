 using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Talabat.Core.Entites.Identity;
using Talabat.Core.IRepositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using TalabatAPIs.Errors;
using TalabatAPIs.Exctensions;
using TalabatAPIs.Helpers;
using TalabatAPIs.Middlewares;

namespace TalabatAPIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwagger();
            
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // AddSingleton() => Liftime is per Runtime
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            #region Cors Policy
            // To Allow other Orgins to connect with the APIs and hass access to them
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });
            #endregion

            var app = builder.Build();

            #region Updatedatabase
            //_____________________Update-Database_______________________

            // (using) For Dispose Automatically after Update
            using var Scope = app.Services.CreateScope();
            //Group of Services Lifetime Scooped
            var Services = Scope.ServiceProvider;
            //Services itself
            var loogerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                //Ask CLR to Creating object from DbContext Explicitly 
                var dbContext = Services.GetService<StoreDbContext>();
                //Update-Database
                await dbContext.Database.MigrateAsync();

                /* For Identity */
                var IdentityDbContext = Services.GetService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync(); // To Update-datebase on AppIdentityDbContext
                var UserManger = Services.GetService<UserManager<AppUser>>();
                await AppIdentityDbContextSeeding.SeedUserAsync(UserManger);


                /*Calling Seeding Function */
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var looger = loogerFactory.CreateLogger<Program>();
                looger.LogError(ex, "An Error Occured During Updating The Database");
                //Error massage if something went wrong
            }
            //_____________________Update-Database_______________________
            #endregion

            app.UseMiddleware<ExceptionMiddleWare>(); 
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
          

            app.AddSwagger();

            app.UseStatusCodePagesWithRedirects("/errors/{0}"); // For handling the Responce if the user want to access an Endpoint that not exicts
            app.UseStaticFiles(); // For Pictures projection ater Resolving
            app.UseHttpsRedirection();

            app.UseCors("MyPolicy"); // To Allow other Orgins to connect with the APIs and hass access to them

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
