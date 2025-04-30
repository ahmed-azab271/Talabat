using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandWithTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandWithTypeSpecification(ProductSpecParms Parms) 
            : base( P=> 
            (string.IsNullOrEmpty(Parms.Search) || P.Name.ToLower().Contains(Parms.Search)) // Contains() is best in Search
            &&
            (!Parms.BrandId.HasValue || P.ProductBrandId == Parms.BrandId)
            &&
            (!Parms.TypeId.HasValue || P.ProductTypeId == Parms.TypeId)) /*(Lambda Expression) return SINGLE combined Exprssion not two separite expression */
        {
            Includes.Add(P=>P.ProductBrand);
            Includes.Add(P=>P.ProductType);

            if(!string.IsNullOrEmpty(Parms.Sort))
            {
                switch (Parms.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P=>P.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDes(P=>P.Price);
                        break;
                    default:
                        AddOrderBy(N => N.Name);
                        break;
                }
            }

            ApplyPagination(Parms.PageSize * (Parms.PageIndex - 1), Parms.PageSize);

        }
        public ProductWithBrandWithTypeSpecification(int id) :base(P=>P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
