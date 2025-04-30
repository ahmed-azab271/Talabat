using Talabat.Core.Entites.Order;

namespace TalabatAPIs.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
