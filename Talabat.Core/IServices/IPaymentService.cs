using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order;

namespace Talabat.Core.IServices
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string BasketId);
        Task<Order> UpdatePaymentIntentToSuccessOrFaild(string PaymentIntent, bool Flag); 
    }
}
