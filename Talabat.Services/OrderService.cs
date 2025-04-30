using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order;
using Talabat.Core.IRepositories;
using Talabat.Core.IServices;
using Talabat.Core.Specification;
using Talabat.Core.Specification.OrderSpecification;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepo basketRepo;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IBasketRepo _basketRepo , IUnitOfWork _unitOfWork)
        {
            basketRepo = _basketRepo;
            unitOfWork = _unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //Get Basket From Basket Repo
            var Basket = await basketRepo.GetBasketAsync(BasketId);

            //Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            if(Basket?.Items.Count>0)
            {
                foreach(var item in Basket.Items)
                {
                    var Product = await unitOfWork.CreateRepo<Product>().GetByIdAsync(item.Id);
                    var Orderitem = new OrderItem(item.Id, Product.Name, Product.PictureUrl, Product.Price, item.Quantity);
                    OrderItems.Add(Orderitem);
                }
            }

            //Calculate SubTotal
            var SubToltal = OrderItems.Sum(Item => Item.Quantity * Item.Price);

            //Get Delivery Method From DeliveryMethod Repo
            var DeliveryMethod = await unitOfWork.CreateRepo<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            //Create Order
            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, SubToltal , Basket.PaymentIntentId);

            //Add Order Locally
            await unitOfWork.CreateRepo<Order>().AddAsync(Order);

            //Save Order To Database[ToDo]
            var Result = await unitOfWork.CompeleteAsync();
            if(Result <= 0 ) return null;
            return Order;
        }

        public async Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpec(BuyerEmail);
            var Orders = await unitOfWork.CreateRepo<Order>().GetAllWithSpecAsync(Spec);
            return Orders;
        }

        public async Task<Order?> GetOrdersForUserByIdAsync(string BuyerEmail, int OrderId)
        {
            var Spec = new OrderSpec(BuyerEmail , OrderId);
            var Orders = await unitOfWork.CreateRepo<Order>().GetByIdWithSpecAsync(Spec);
            return Orders;
        }
    }
}
