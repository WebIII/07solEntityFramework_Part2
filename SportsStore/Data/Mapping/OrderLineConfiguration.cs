using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;


namespace SportsStore.Data.Mapping
{
    internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {

        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.ToTable("OrderLine");
            builder.HasKey(t => new { t.OrderId, t.ProductId });

            builder.HasOne(t => t.Product).WithMany().IsRequired().HasForeignKey(t => t.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
