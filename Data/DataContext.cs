using dotnet_nopreco.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_nopreco.Data
{
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            user.Property(x => x.Name).HasColumnName("name").IsRequired();
            user.Property(x => x.Email).HasColumnName("email").IsRequired();
            user.Property(x => x.CreatedAt).HasColumnName("createdAt").IsRequired();

            var product = modelBuilder.Entity<Product>();
            product.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            product.Property(x => x.Name).HasColumnName("name").IsRequired();
            product.Property(x => x.Description).HasColumnName("description").IsRequired();
            product.Property(x => x.ImageUrl).HasColumnName("imageUrl").IsRequired();
            product.Property(x => x.Price).HasColumnName("price").IsRequired();
            product.Property(x => x.Category).HasColumnName("category").IsRequired();
            product.Property(x => x.UpdatedAt).HasColumnName("updatedAt").IsRequired();
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
    }
}