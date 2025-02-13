using SmartParkingSystem.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartParkingSystem.Entities.DataTransferObjects;

namespace SmartParkingSystem.Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasOne(b => b.Driver).WithMany().HasForeignKey(b => b.DriverId);
            modelBuilder.Entity<Booking>().HasOne(b => b.ParkingSpace).WithMany().HasForeignKey(b => b.SpaceId);
        }
        public DbSet<ParkingOwner> ParkingOwners { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
