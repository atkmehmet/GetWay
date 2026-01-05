
using Microsoft.EntityFrameworkCore;

namespace Getway.Infrastructure
{
    public class AppDb:DbContext
    {
       public  AppDb(DbContextOptions<AppDb> options): base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
