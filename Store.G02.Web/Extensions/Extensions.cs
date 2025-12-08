using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Persistence;
using Store.G02.Persistence.Identity.Contexts;
using Store.G02.Services;
using Store.G02.Shared;
using Store.G02.Shared.ErrorModels;
using Store.G02.Web.Middlewares;
using System.Text;

namespace Store.G02.Web.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddWebServices();
            services.ConfigureApiBehaviourOptions();

            //Allow Dependancy injection For DbContext 
            //and make Connection String
            /* InfrastructureServicesRegistration
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                //appsettings
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                
             });

            //Allow DI for Database_Initializer
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           */

            services.AddInfrastructureServices(configuration);


            /*ApplicationServicesRegistration
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile(builder.Configuration)));
            */

            services.AddApplicationServices(configuration);

            ConfigureApiBehaviourOptions(services);

            services.AddIdentityServices();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddAuthenticationService(configuration);

            // Policies for enable the frontEnd to send Requests to the backEnd
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }

        private static IServiceCollection ConfigureApiBehaviourOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContex) =>
                {
                    //I need the Models who has Error Not all models 
                    //so filter with where
                    var errors = actionContex.ModelState.Where(M => M.Value.Errors.Any()) //ModelState
                                                        .Select(M => new ValidationError()
                                                        {
                                                            Field = M.Key,
                                                            Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                                        }).ToList();
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);

                };

            });
            return services;
        }

        private static IServiceCollection AddAuthenticationService(this IServiceCollection services , IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            //Add Authentication
            //Validation of TOKEN
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //These Parameters to check the TOKEN is vaild or Not
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),

                };
            });
            return services;
        }


        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            //object from _userManager & _roleManager
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true; //Must Be unique Email

            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();

            return services;
        }


        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {

            app.UseGlobalErrorHandling();

            //To Allow Static Files in the server 
            //Configure this MidlleWare
            app.UseStaticFiles();

            await app.SeedData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();


            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            //Ask From CLR
            #region Initialize Db
            // use using because it's unmanaged Resource and to make it safe use
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); //Ask CLR To Create Object From IDbInitializer
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();

            #endregion
            return app;
        }
    
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

    }
}
