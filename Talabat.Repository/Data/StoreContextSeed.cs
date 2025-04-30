using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var BrandString = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                // to Cast the file from String to List 
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandString);
                if (Brands?.Count() > 0)
                {
                    foreach (var Brand in Brands)
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                    await dbContext.SaveChangesAsync();
                }
            }
            if(!dbContext.ProductTypes.Any())
            {
                var TypeString = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeString);
                if (Types?.Count() > 0)
                {
                    foreach (var Type in Types)
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    await dbContext.SaveChangesAsync();
                }
            }
            if(!dbContext.Products.Any())
            {
                var ProductString = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductString);
                if(Products?.Count() > 0)
                {
                    foreach(var Product in Products)
                        await dbContext.Set<Product>().AddAsync(Product);
                    await dbContext.SaveChangesAsync();
                }
            }
            if(!dbContext.DeliveryMethods.Any())
            {
                var DeliveryMethodsString = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsString);
                if(DeliveryMethods?.Count>0)
                {
                    foreach (var Method in DeliveryMethods)
                        await dbContext.Set<DeliveryMethod>().AddAsync(Method);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
