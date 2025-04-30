using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order
{
    public class DeliveryMethod : BaseEntity
    {
        // To Migrate without Errors
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal coast)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Coast = coast;
        }

        public string  ShortName { get; set; }
        public string  Description { get; set; }
        public string  DeliveryTime { get; set; }
        public decimal  Coast { get; set; }
    }
}
