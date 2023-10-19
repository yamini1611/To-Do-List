using Microsoft.EntityFrameworkCore;

namespace To_Do_List.Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
    }
}
