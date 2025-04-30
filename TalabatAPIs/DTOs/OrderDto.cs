using Talabat.Core.Entites.Order;

namespace TalabatAPIs.DTOs
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DelivaryMethodId { get; set; }
        public Address ShippingAddress { get; set; }
    }
}
