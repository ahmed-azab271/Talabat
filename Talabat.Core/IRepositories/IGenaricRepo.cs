using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Core.IRepositories
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);



        // For Specifications Design Pattern
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);

    }
}
