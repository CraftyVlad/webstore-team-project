using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.Models.Entities;

namespace web.Models.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(b => b.Content)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(b => b.Rating)
                .IsRequired()
                .HasDefaultValue(0);
            builder.HasOne(b => b.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(b => b.ItemId);
            builder.Property(c => c.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
