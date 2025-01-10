using DecBatchCodeFirstApproachImpl.Models;
using Microsoft.EntityFrameworkCore;

namespace DecBatchCodeFirstApproachImpl.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Employee> emp { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
