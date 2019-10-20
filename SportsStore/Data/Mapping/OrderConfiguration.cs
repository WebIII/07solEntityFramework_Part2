using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;


namespace SportsStore.Data.Mapping
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(t => t.OrderId);
            builder.Property(t => t.ShippingStreet).IsRequired().HasMaxLength(100);

            builder.HasOne(t => t.ShippingCity).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.OrderLines).WithOne().IsRequired().HasForeignKey(t => t.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
