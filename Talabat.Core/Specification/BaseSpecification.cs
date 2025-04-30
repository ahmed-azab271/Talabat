using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set ; } = new List<Expression<Func<T, object>>>(); // For not Repeating the Code
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDes { get ; set ; }
        public int Skip { get ; set; }
        public int Take { get ; set; }
        public bool IsPaginationEnable { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public void AddOrderBy (Expression<Func<T,object>> expression) => OrderBy = expression;

        public void AddOrderByDes(Expression<Func<T, object>> expression) => OrderByDes = expression;
        public void ApplyPagination (int skip , int take )
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
