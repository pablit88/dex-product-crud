
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

            builder.HasMany(p => p.Categories)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProductCategory",
                j => j.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryID")
                    .HasConstraintName("FK_ProductCategory_Category")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Product>()
                    .WithMany()
                    .HasForeignKey("ProductID")
                    .HasConstraintName("FK_ProductCategory_Product")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("ProductCategory");
                    j.HasKey("ProductID", "CategoryID");
                }
            );

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ProductID")
                .ValueGeneratedOnAdd();
        }
    }
}
