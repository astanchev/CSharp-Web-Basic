namespace PandaApp
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DataSettings.Connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Package>()
                .HasOne(u => u.Recipient)
                .WithMany(p => p.Packages)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Receipt>()
                .HasOne(u => u.Recipient)
                .WithMany(r => r.Receipts)
                .HasForeignKey(r => r.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Receipt>()
                .HasOne(r => r.Package)
                .WithOne();
        }
    }
}
