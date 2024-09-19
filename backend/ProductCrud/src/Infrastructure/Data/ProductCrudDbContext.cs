using Dex.ProductCrud.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dex.ProductCrud.Infrastructure.Data
{
    public class ProductCrudDbContext: DbContext
    {
        public ProductCrudDbContext(DbContextOptions<ProductCrudDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
