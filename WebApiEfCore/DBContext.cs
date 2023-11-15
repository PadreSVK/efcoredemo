using Microsoft.EntityFrameworkCore;

namespace WebApiEfCore
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }


    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public class User : IEntity
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public required string Name { get; set; }
    }
}
