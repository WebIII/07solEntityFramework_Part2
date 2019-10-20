using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;


namespace SportsStore.Data.Mapping
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(t => t.CategoryId);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        }
    }
}
