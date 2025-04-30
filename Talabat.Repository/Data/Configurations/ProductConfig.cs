using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                    .WithMany();
                    //.OnDelete(DeleteBehavior.SetNull); If i delete the ProductBrand all ProductBrandId will set as null if it accepted it as property
             
            builder.HasOne(P=>P.ProductType)
                    .WithMany();
        }
    }
}
