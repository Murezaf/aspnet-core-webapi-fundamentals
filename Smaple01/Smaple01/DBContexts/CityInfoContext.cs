using Microsoft.EntityFrameworkCore;
using Smaple01.Entities;
using Smaple01.Models;

namespace Smaple01.DBContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> cities { get; set; }
        public DbSet<PointOfInterest> pointOfInterests { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("NewYork") { Id = 1, Description = "best place to live in" },
                new City("London") { Id = 2, Description = "always rainy" },
                new City("Paris") { Id = 3, Description = "most romance city" }
                );

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("FreedomStatus")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "gourgeos"
                },
                new PointOfInterest("Parks")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "clean"
                },
                new PointOfInterest("Clock")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "big"
                },
                new PointOfInterest("PhoneStations")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "red"
                },
                new PointOfInterest("Eiffel")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "Wonderful"
                }
                );

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
