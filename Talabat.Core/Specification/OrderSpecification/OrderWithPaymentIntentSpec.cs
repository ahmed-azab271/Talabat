using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order;

namespace Talabat.Core.Specification.OrderSpecification
{
    public class OrderWithPaymentIntentSpec : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpec(string paymnetIntentId):base (P=>P.PaymentIntendId == paymnetIntentId)
        {
            Includes.Add(I => I.DeliveryMethod);
            Includes.Add(I => I.OrderItems);
            AddOrderByDes(D => D.OrderDate);
        }
    }
}
