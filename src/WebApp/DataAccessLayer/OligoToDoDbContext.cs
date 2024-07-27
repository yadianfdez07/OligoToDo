using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApp.DataAccessLayer
{
    public class OligoToDoDbContext: DbContext
    {
        public DbSet<TaskModel> Tasks{ get; set; }
        public DbSet<UserModel> Users { get; set; }

        public OligoToDoDbContext(DbContextOptions<OligoToDoDbContext> options) : base(options)
        {
        }
    }
}
