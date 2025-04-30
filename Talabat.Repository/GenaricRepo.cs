using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly StoreDbContext dbContext;

        public GenaricRepo(StoreDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync() =>  await dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity) => await dbContext.Set<T>().AddAsync(entity);

        public void Update(T entity) =>  dbContext.Set<T>().Update(entity);

        public void Delete(T entity) => dbContext.Set<T>().Remove(entity);




        // For Specifications Design Pattern
        private IQueryable<T> ApplaySpecifications(ISpecification<T> Spec) 
            => SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), Spec); 

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplaySpecifications(Spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplaySpecifications(Spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> Spec)
        {
            return await ApplaySpecifications(Spec).CountAsync();
        }
    }
}
