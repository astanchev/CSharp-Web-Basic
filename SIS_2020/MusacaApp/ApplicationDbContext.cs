namespace MusacaApp
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DataSettings.Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Order>()
                .HasKey(order => order.Id);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.Products)
                .WithOne(orderProduct => orderProduct.Order)
                .HasForeignKey(orderProduct => orderProduct.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Cashier);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(orderProduct => new {orderProduct.OrderId, orderProduct.ProductId});
        }
    }
}
