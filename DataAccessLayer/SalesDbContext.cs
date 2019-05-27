using BusinessEntities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MVC.DataAccessLayer
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public SalesDbContext() : base("Data Source=(local);Initial Catalog=SalesERPDB;Integrated Security=True")
        {
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 120;
        }
        ///public List<SalesDbContext> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            base.OnModelCreating(modelBuilder);
        }
    }
}
