using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;


namespace SportsStore.Data.Mapping
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(t => t.CustomerId);
            builder.Property(t => t.CustomerName).IsRequired().HasMaxLength(20);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            builder.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Street).HasMaxLength(100);

            builder.HasOne(t => t.City).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Orders).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
