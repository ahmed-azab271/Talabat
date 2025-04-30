using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pined")]
        Pending ,
        [EnumMember(Value = "PaymentRecived")]
        PaymentRecived,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
    }
}
