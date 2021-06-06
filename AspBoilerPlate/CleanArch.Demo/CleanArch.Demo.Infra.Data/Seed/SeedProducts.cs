using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.Extensions.Logging;

namespace CleanArch.Demo.Infra.Data.Seed
{
   public class SeedProducts
    {
        public static async Task SeedAsync(UniversityDBContext context, ILoggerFactory loggerFactory)
        {
            try {

               
                string path = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, @"..\..\..\..\"));
                
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText(path + @"/CleanArch.Demo.Infra.Data/Seed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(path + @"/CleanArch.Demo.Infra.Data/Seed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(path + @"/CleanArch.Demo.Infra.Data/Seed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }

            catch ( Exception ex)
            {
                var logger = loggerFactory.CreateLogger<SeedProducts>();
                logger.LogError(ex.Message);
            }
        }
    }
}
