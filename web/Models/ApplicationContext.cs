using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using web.Models.Configurations;
using web.Models.Entities;

namespace web.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var config = new ConfigurationBuilder()
        //    //    .SetBasePath(Directory.GetCurrentDirectory())
        //    //    .AddJsonFile("Data/appsettings.json")
        //    //    .Build();
        //    //string connStr = config.GetConnectionString("SqlLiteConnection");
        //    //optionsBuilder.UseSqlite(connStr);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ������������ Fluent API
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new ItemCategoryConfiguration());
        }
    }
}
