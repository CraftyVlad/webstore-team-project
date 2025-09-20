using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.Models.Entities;

namespace web.Models.Configurations
{
    public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            // - Кто ты воин?
            // - Aхиллес, сын Пелея!
        }
    }
}
