using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{

    //CLR => Primary Constructor
    public class DbInitializer(
        StoreDbContext _context,
        IdentityStoreDbContext _identityDbContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
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

            if(!_context.DeliveryMethods.Any())
            {

                //DeliveryMethods


                //1. Read All Data From Json File 'delivery.json'
                //Store.G02\Infrastructure\Store.G02.Persistence\Data\DataSeeding\delivery.json
                //Note That (../)

                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\delivery.json");

                //2. Convert The JsonString To List<ProductBrand> 

                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);


                //Add List To The Db
                if (DeliveryMethods is not null && DeliveryMethods.Count > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(DeliveryMethods);
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

        public async Task InitializeIdentityAsync()
        {
            //Create Db =>
            //Update Db =>

            //If There is any migration not applied
            if (_identityDbContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            //Data Seed
            if(!_identityDbContext.Roles.Any())
            {
                //Add Role
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });//???????
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            }

            if(!_identityDbContext.Users.Any())
            {
                //Add User
                var superAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01233345555"
                };
                
                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01233345555"
                };

                //Add 2 Users password
                await _userManager.CreateAsync(superAdmin, "P@ssW0rd");
                await _userManager.CreateAsync(admin, "P@ssW0rd");

                //Assign Role
                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");


            }
        }
    }
}
