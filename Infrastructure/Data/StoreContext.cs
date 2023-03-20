using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        // options includes the connection string which is set in: appsettings.Development.json
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }   

        // Tell data context about our entity and bring in using API.Entities
        public DbSet<Product> Products { get; set; } //Entity Framework has "Products" as the name of the table

        // Two new tables
        // Products (above) will have a foreign key pointing to these two.
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // Inform this class that there are configurations to look for by overriding one of the methods
        // When we create a migration OnModelCreating is the class responsible for doing that 
        // We override it and tell i to look for our configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call to our base class (Dbcontext)
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Convert decimal values to doubles
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }

        }
    }
}


