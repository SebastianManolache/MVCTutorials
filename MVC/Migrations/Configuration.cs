using MVC.DataAccessLayer;
using System.Data.Entity.Migrations;

namespace MVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SalesDbContext context)
        {
        }
    }
}
