using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Order = Store.G02.Domain.Entities.Orders.Order;

namespace Store.G02.Persistence.Data.Contexts
{
    //Step 1: DbSet For The tables that i want to Map in DB.
    //Step 2: Configurations for All the 3.
    public class StoreDbContext : DbContext //instsall Package for DbContext
    {

        //Step 4 : Constructor 

        //Step 5 : Allow Dependancy injection in program in services ...

        //Step 6 : Install package for migration Add-Migration.... => install it in the project who is startup project
        //PM> Add-Migration "InitialCreate" -OutputDir Data/Migration
        //Dont't Forget to Detect the Default Project That has the DbContext
        public StoreDbContext (DbContextOptions<StoreDbContext> options) : base(options) //??
        {
              
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        //Step 3: Override on the function on Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Instead of Doing this 
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());


            //We Do this
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Apply on this Project Only (Assembly of this Project)
            base.OnModelCreating(modelBuilder);
        }

    }
}
