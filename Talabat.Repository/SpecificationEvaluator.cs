using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<T> where T : BaseEntity
    {
        // Function To Build Query
        // returns IQueryable to implemint these Expressions in database NOT in code (if we used IEnamerbale)
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> Spec)
        {
            var Query = inputQuery; // _dbContext.Product
            

            if (Spec.Criteria is not null) 
                Query = Query.Where(Spec.Criteria); // _dbContext.Product.Where(P => P.Id == id)
           

            if(Spec.OrderBy is not null)
                Query = Query.OrderBy(Spec.OrderBy);
            if(Spec.OrderByDes is not null)
                Query = Query.OrderByDescending(Spec.OrderByDes);

            if (Spec.IsPaginationEnable)
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);


            Query = Spec.Includes.Aggregate(Query, (inputQuery, currentQuery) => inputQuery.Include(currentQuery)); 
            //_dbContext.Product.Where(P => P.Id == id).Include(X=>X.ProductType).Include(Y=>Y.ProductBrand)

            return Query;
        }
    }
}
