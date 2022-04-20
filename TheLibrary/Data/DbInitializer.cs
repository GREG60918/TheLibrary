using Microsoft.EntityFrameworkCore;

using TheLibrary.Models;

namespace TheLibrary.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<MembershipType>().HasData(
                   new MembershipType() { Id = 1, Name = "Pay as You Go", SignupFee = 0, DurationInMonths = 0, DiscountRate = 0 },
                   new MembershipType() { Id = 2, Name = "Monthly", SignupFee = 30, DurationInMonths = 1, DiscountRate = 10 },
                   new MembershipType() { Id = 3, Name = "Quarterly", SignupFee = 40, DurationInMonths = 3, DiscountRate = 15 },
                   new MembershipType() { Id = 4, Name = "Yearly", SignupFee = 300, DurationInMonths = 12, DiscountRate = 20 });

            modelBuilder.Entity<Genre>().HasData(
                   new Genre() { Id = 1, Name = "Comedy" },
                   new Genre() { Id = 2, Name = "Action" },
                   new Genre() { Id = 3, Name = "Romance" },
                   new Genre() { Id = 4, Name = "Horror" });
        }
    }
}
