using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int productId, string productName, string pictureUrl, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        public string  PictureUrl { get; set; }
        public decimal  Price { get; set; }
        public int  Quantity { get; set; }
    }
}
