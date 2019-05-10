using MVC.DataAccessLayer;
using System.Data.Entity.Migrations;

namespace MVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SalesContext context)
        {
        }
    }
}
