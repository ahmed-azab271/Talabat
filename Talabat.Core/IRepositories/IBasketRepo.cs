using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.IRepositories
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync(string BasketId);
        Task<CustomerBasket?> CreateUpdateBasketAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
