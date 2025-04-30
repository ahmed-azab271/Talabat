using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.Core.IServices;
using TalabatAPIs.DTOs;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    public class PaymentsController : APIBaseController
    {
        private readonly IPaymentService paymentService;
        //const  string endpointSecret = "whsec_ct6caff3f9fb1d4e28b1d1478921288a4b20c67ab596674c9bff4d31876cf2be";

        public PaymentsController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CustomerBasketDto) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponce) , StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerBasketDto?>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Basket = await paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (Basket is null) return BadRequest(new ApiResponce(400, "There is a prblem with your basket"));
            return Ok(Basket);
        }

        //[HttpPost("webhook")]
        //public async Task<IActionResult> StripeWebHook()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        //    try
        //    {
        //        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],endpointSecret);

        //        // Handle the event
        //        if (stripeEvent.Type == Events.)
        //            paymentService.UpdatePaymentIntentToSuccessOrFaild();
                
        //        else if (stripeEvent.Type == Events.)
        //            paymentService.UpdatePaymentIntentToSuccessOrFaild();

        //        return Ok();
        //    }
        //    catch (Exception ex){ return BadRequest(ex.Message);}
        //}
    }
}
