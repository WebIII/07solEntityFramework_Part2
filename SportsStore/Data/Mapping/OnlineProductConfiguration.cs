using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;


namespace SportsStore.Data.Mapping
{
    internal class OnlineProductConfiguration : IEntityTypeConfiguration<OnlineProduct>
    {

        public void Configure(EntityTypeBuilder<OnlineProduct> builder)
        {
            builder.Property(t => t.ThumbNail).IsRequired().HasMaxLength(100);
        }
    }
}
