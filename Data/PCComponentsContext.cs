using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PCConfigurator.Data;

namespace PCConfigurator.Data
{
    public class PCComponentsContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComponentEntity> Components { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // Новые сущности:
        public DbSet<Order> Orders { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Используем локальную базу данных; проверьте строку подключения перед использованием
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PCComponentsDB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specification>()
                .HasOne(s => s.Component)
                .WithMany(c => c.Specifications)
                .HasForeignKey(s => s.ComponentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Настраиваем составной ключ для UserFavorite
            modelBuilder.Entity<UserFavorite>()
                .HasKey(uf => new { uf.UserId, uf.ComponentID });
        }
    }
}
