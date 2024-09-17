using Dex.ProductCrud.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dex.ProductCrud.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("CategoryID");
        }
    }
}
