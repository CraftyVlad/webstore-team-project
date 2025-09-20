using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.Models.Entities;

namespace web.Models.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasMany(t => t.Items)
            .WithMany(b => b.Categories)
            .UsingEntity<ItemCategory>(
                configureLeft: etb => etb
                    .HasOne(bt => bt.Category)
                    .WithMany(t => t.ItemCategories)
                    .HasForeignKey(bt => bt.CategoryId),
                configureRight: etb => etb
                    .HasOne(bt => bt.Item)
                    .WithMany(b => b.ItemCategories)
                    .HasForeignKey(bt => bt.ItemId),
                configureJoinEntityType: bt =>
                {
                    bt.HasKey(x => new { x.CategoryId, x.ItemId });
                    bt.ToTable("BlogThemes");
                }
            );
        }
    }
}
