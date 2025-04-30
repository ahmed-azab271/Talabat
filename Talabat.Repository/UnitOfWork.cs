using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext dbContext;
        private Hashtable repositories;

        public UnitOfWork(StoreDbContext _dbContext)
        {
            dbContext = _dbContext;
            repositories = new Hashtable();
        }
        public IGenaricRepo<T> CreateRepo<T>() where T : BaseEntity
        {
            var type = typeof(T).Name; // as (Product)

            // to check if the Repo alredy created before or not created at all 
            if (!repositories.ContainsKey(type))
            {
                var Repo = new GenaricRepo<T>(dbContext);
                repositories.Add(type, Repo);
            }
            return repositories[type] as IGenaricRepo<T>;
        }
        public async Task<int> CompeleteAsync() => await dbContext.SaveChangesAsync();
        public  ValueTask DisposeAsync() =>  dbContext.DisposeAsync();
    }
}
