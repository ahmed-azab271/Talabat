using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepo<T> CreateRepo<T>() where T : BaseEntity;
        Task<int> CompeleteAsync();
    }
}
