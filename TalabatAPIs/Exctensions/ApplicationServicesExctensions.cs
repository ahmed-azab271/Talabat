using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Repository;
using Talabat.Services;
using TalabatAPIs.Errors;
using TalabatAPIs.Helpers;

namespace TalabatAPIs.Exctensions
{
    // To make Exctension methode must be in Static class 
    public  static class ApplicationServicesExctensions
    {
        //to call the mehode like (builder.Services.AddApplicationServices) MUST put (this) to make IServiceCollection is the CALLER that i can call my method throw  
        public static IServiceCollection AddApplicationServices (this IServiceCollection Services)
        {
            /*To Make it Genaric Dependancy*/
            Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            Services.AddSingleton(typeof(ICachedService), typeof(CachedService));

            Services.AddAutoMapper(typeof(MappingProfiles));  // == builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            //To Edit the Default of (Validation Error Responce) 
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                       .SelectMany(P => P.Value.Errors)
                                                       .Select(E => E.ErrorMessage)
                                                       .ToArray();
                    var ValidationErrorResponce = new ApiValidationErrorResponce()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(ValidationErrorResponce);
                };
            });
            return Services;
        }
    }
}
