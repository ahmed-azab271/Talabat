using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string BuyerEmail);
        Task<Order?> GetOrdersForUserByIdAsync (string BuyerEmail , int OrderId);
    }
}
