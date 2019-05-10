using MVC.Models;
using System.Data.Entity;


namespace MVC.DataAccessLayer
{
    public class SalesContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public SalesContext() : base("SalesERPDAL")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            base.OnModelCreating(modelBuilder);
        }
    }
}