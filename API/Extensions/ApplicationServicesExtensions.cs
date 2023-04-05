using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    // Extension Methods have to be static
    public static class ApplicationServicesExtensions
    {
        // Extension method
        // First parameter is the thing we are extending
        // In this case it is the thing we are returning: an IServiceCollection
        // Also add services that need configuration (2nd parameter)
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            // Set up database context. StoreContext is the name of the class in StoreContext.cs
            // opt => is a lambda. Execute commands in between {} based on opt
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // IConnectionMultiplexer is the API we will use to connect to a Redis database
            // Redis pronounced "Readis"
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddScoped<IBasketRepository, BasketRepository>(); 
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddScoped<ITokenService, TokenService>();

            // Generic Repository doesn't have a type yet
            // need to use the typeof command for the Interface and the class and use empty <>
            // This will register it as a service
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Use below for the mapping service setup in MappingProfiles.cs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Override ApiController for some http status
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    // Now have access to the model state
                    var errors = ActionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)           
                        // Get the specific error
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors // errors is the array from above
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}