using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.Models.Entities;

namespace web.Models.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(240);
            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Review)
                .WithMany(b => b.Replies)
                .HasForeignKey(c => c.ReviewId);
            builder.Property(c => c.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
