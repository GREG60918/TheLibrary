using Microsoft.EntityFrameworkCore;

using TheLibrary.Models;

namespace TheLibrary.Data
{
    public class TheLibraryDbContext : DbContext
    {
        public TheLibraryDbContext()
        {
        }

        public TheLibraryDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<MembershipType> MembershipTypes { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-C3KJ59F\\SQLEXPRESS;Database=TheLibraryDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<MembershipType>().ToTable("MembershipTypes");
            modelBuilder.Entity<Genre>().ToTable("Genres");

            new DbInitializer(modelBuilder).Seed();
        }
    }
}
