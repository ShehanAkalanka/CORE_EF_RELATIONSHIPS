using Microsoft.EntityFrameworkCore;

namespace NET_CORE_EF_RELATIONSHIPS.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Weapon> weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

    }
}
