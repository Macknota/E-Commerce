using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{

    //CLR => Primary Constructor
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {

        //Step 1 : To Do Any operation on Db U need object from the class that Represent Db
        //Make CLR Create The Object By The Constructor
       
        //private readonly StoreDbContext _context;
        //public DbInitializer(StoreDbContext context)
        //{
        //    _context = context;
        //}


        //Step 2 :
        public async Task InitializeAsync()
        {
            //Create Db =>
            //Update Db =>

            //If There is any migration not applied
            if(_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }

            //Data Seeding
            //Must be the table Empty

            if(!_context.ProductBrands.Any())
            {

                //ProductBrands


                //1. Read All Data From Json File 'brands.json'
                //Store.G02\Infrastructure\Store.G02.Persistence\Data\DataSeeding\brands.json
                //Note That (../)

                var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\brands.json");

                //2. Convert The JsonString To List<ProductBrand> 

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);


                //Add List To The Db
                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }
            }


            //ProductTypes
            if (!_context.ProductTypes.Any())
            {
                //Step 1 :
                var typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\types.json");

                //Step 2 :
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                //Step 3 :
                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }

            }


            //Product
            if (!_context.Products.Any())
            {
                //Step 1 :
                var productdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\products.json");

                //Step 2 :
                var products = JsonSerializer.Deserialize<List<Product>>(productdata);

                //Step 3 :
                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }

            }
            await _context.SaveChangesAsync();
        }
    }
}
