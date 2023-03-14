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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

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

            //customer
            var customerEntity = modelBuilder.Entity<Customer>();
            //has a primary key Id
            customerEntity.HasKey(customer => customer.Id); 
            //has one-to-one relation with an Order
            customerEntity.HasOne(customer => customer.Order)
                          .WithOne(order => order.Customer)
                          .HasForeignKey<Order>(order => order.CustomerId); //when using a one-to - one relation, Entity Framework Core needs to know which side is the master in the relation, and that is why you need to add a foreign key to the Order class

            //order
            var orderEntity = modelBuilder.Entity<Order>();
            //has a primary key Id
            orderEntity.HasKey(order => order.Id);
            //has a many-to-one relationship with a PizzaOrder
            orderEntity.HasMany(order => order.PizzaOrders)
                       .WithOne(pizzaOrder => pizzaOrder.Order);

            //pizza order
            //indicate that a PizzaOrder has one pizza and that one pizza can belong to many orders
            modelBuilder.Entity<PizzaOrder>()
                        .HasOne(po => po.Pizza)
                        .WithMany();
        }
    }
}
