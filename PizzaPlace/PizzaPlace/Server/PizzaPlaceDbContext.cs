using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaPlace.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.Intrinsics.X86;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PizzaPlace.Server
{
    public class PizzaPlaceDbContext : DbContext
    {
        // a constructor taking an DbContextOptions<PizzaPlaceDbContext> argument
        // this is used to pass the connection to the server
        public PizzaPlaceDbContext(DbContextOptions<PizzaPlaceDbContext> options) : base(options)
        {
        }

        // Entity Framework Core use the DbSet<T> to map this collection to a table
        public DbSet<Pizza> Pizzas { get; set; }

        // override the OnModelCreating method, which takes a modelBuilder argument
        // in the OnModelCreating method, you can describe how each DbSet<T> should be mapped to the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var pizzaEntity = modelBuilder.Entity<Pizza>();
            // the Pizza table should have a primary key, the Id property of the Pizza class
            pizzaEntity.HasKey(pizza => pizza.Id);
            // use the SQL Server MONEY type for Pizza.Price property
            pizzaEntity.Property(pizza => pizza.Price).HasColumnType("money");
        }
    }
}
