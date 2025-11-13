
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Services;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Services.Mapping.Products;
using System.Threading.Tasks;

namespace Store.G02.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);//???

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Allow Dependancy injection For DbContext 
            //and make Connection String
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                //appsettings
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                
             });

            //Allow DI for Database_Initializer
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile(builder.Configuration)));



            var app = builder.Build();

           
            //To Allow Static Files in the server 
            //Configure this MidlleWare
            app.UseStaticFiles();



            //Ask From CLR
            #region Initialize Db
            // use using because it's unmanaged Resource and to make it safe use
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //Ask CLR To Create Object From IDbInitializer
            await dbInitializer.InitializeAsync();

            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
