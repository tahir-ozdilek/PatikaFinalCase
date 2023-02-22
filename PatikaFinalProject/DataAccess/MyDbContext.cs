using Microsoft.EntityFrameworkCore;

namespace PatikaFinalProject.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<ShoppingList> MovieType { get; set; }
        public DbSet<Category> Customer { get; set; }
        public DbSet<Product> Movie { get; set; }
    }
}
