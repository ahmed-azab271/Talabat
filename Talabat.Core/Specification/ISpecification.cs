using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        // Signeture for property for (Where) condition That takes (T) and Return (bool)
        public Expression<Func<T,bool>> Criteria { get; set; } // (P => P.id == id) 
        // Signeture for property for (Include) condition 
        public List<Expression<Func<T,object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDes { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnable { get; set; }
    }
}
