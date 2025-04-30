using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Order;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Address [Not Table] => Order + Address => One Table
            builder.OwnsOne(O => O.ShippingAddress, X => X.WithOwner());

            // To Store the OrderStatus in DB as a String and return it from DB as Label of Enum
            builder.Property(X => X.OrderStatus)
                .HasConversion(S => S.ToString(), O => (OrderStatus)Enum.Parse(typeof(OrderStatus), O));

            // if i deleted the DeliveryMethods nothing will happen to the Oreders that used this DeliveryMethode
            builder.HasOne(M => M.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
