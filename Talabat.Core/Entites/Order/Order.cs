using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, decimal subTotal, string? paymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntendId = paymentIntendId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset  OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShippingAddress { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Coast;

        public string? PaymentIntendId { get; set; }
    }
}
