
using Dex.ProductCrud.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dex.ProductCrud.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(p => p.Image)
                .IsRequired()
                .HasMaxLength(64);

            //builder.HasMany(p => p.Categories)
            //    .WithMany(c => c.Products)
            //    .UsingEntity(
            //    j => j.ToTable("ProductCategory"));

            builder.HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategory", // Name of the join table
                j => j.HasOne<Category>() // Link to Category
                    .WithMany()
                    .HasForeignKey("CategoryID") // Configure foreign key
                    .HasConstraintName("FK_ProductCategory_Category"),
                j => j.HasOne<Product>() // Link to Product
                    .WithMany()
                    .HasForeignKey("ProductID") // Configure foreign key
                    .HasConstraintName("FK_ProductCategory_Product"),
                j =>
                {
                    j.ToTable("ProductCategory"); // Join table name
                    j.HasKey("ProductID", "CategoryID"); // Composite primary key
                }
            );

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ProductID");
        }
    }
}
