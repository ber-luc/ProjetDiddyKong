using Microsoft.EntityFrameworkCore;

namespace clicker.database
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options){}
    }
}