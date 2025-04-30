using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specification.OrderSpecification;
using Product = Talabat.Core.Entites.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    { 
        private readonly IConfiguration _configuration;
        private readonly IBasketRepo _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration configuration, IBasketRepo basicRepository, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _basketRepository = basicRepository;
        _unitOfWork = unitOfWork;
    }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null;

            // Calculate Shipping Price
            decimal shippingPrice = 0M;
            if (Basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.CreateRepo<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Coast ;
            }

            // Validate and update item prices
            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.CreateRepo<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }
            var subtotal = Basket.Items.Sum(item => item.Price * item.Quantity);

            // Handle Payment Intent
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(Basket.PaymentIntentId)) // Create new Payment Intent
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(subtotal + shippingPrice) * 100,  //Convert to Cent
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update existing Payment Intent
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(subtotal + shippingPrice) * 100
                };
                paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId, options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            // Update basket and return
            await _basketRepository.CreateUpdateBasketAsync(Basket);
            return Basket;
        }

        public async Task<Order> UpdatePaymentIntentToSuccessOrFaild(string PaymentIntent, bool Flag)
        {
            var Spec = new OrderWithPaymentIntentSpec(PaymentIntent);
            var order = await _unitOfWork.CreateRepo<Order>().GetByIdWithSpecAsync(Spec);
            if (Flag)
                order.OrderStatus = OrderStatus.PaymentRecived;
            else
                order.OrderStatus = OrderStatus.PaymentFailed;
            _unitOfWork.CreateRepo<Order>().Update(order);
            await _unitOfWork.CompeleteAsync();
            return order;
        }
    }
}
