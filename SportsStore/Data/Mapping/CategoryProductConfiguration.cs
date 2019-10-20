using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;

namespace SportsStore.Data.Mapping
{
    internal class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {

        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(t => new { t.CategoryId, t.ProductId });

            builder.HasOne(pt => pt.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(pt => pt.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Product)
             .WithMany()
             .HasForeignKey(pt => pt.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
