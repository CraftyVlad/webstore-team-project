using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.Models.Entities;

namespace web.Models.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(300)
                .HasDefaultValue("No description provided.");
            builder.Property(b => b.Owner)
                .IsRequired();
            builder.Property(b => b.PriceEth)
                .IsRequired();
            builder.Property(b => b.Stock)
                .IsRequired()
                .HasDefaultValue(1);
            builder.Property(c => c.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
