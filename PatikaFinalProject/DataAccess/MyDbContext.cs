using Microsoft.EntityFrameworkCore;

namespace PatikaFinalProject.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<MovieType> MovieType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
