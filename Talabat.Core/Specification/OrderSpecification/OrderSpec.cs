using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order;

namespace Talabat.Core.Specification.OrderSpecification
{
    public class OrderSpec : BaseSpecification<Order>
    {
        public OrderSpec(string email) : base(O => O.BuyerEmail == email)
        {
            Includes.Add(I => I.DeliveryMethod);
            Includes.Add(I => I.OrderItems);
            AddOrderByDes(D => D.OrderDate);
        }
        public OrderSpec(string email , int OrderId):base(P=>(P.BuyerEmail == email && P.Id == OrderId)) 
        {
            Includes.Add(I => I.DeliveryMethod);
            Includes.Add(I => I.OrderItems);
            AddOrderByDes(D => D.OrderDate);
        }
    }
}
