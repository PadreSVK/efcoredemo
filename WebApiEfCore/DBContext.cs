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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
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
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Phone { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PhoneNumberConfirmed { get; set; }
    }
}
