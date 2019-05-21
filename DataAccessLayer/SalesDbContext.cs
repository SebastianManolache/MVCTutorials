using BusinessEntities;
using System.Data.Entity;


namespace MVC.DataAccessLayer
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public SalesDbContext() : base("SalesERPDAL")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            base.OnModelCreating(modelBuilder);
        }
    }
}
