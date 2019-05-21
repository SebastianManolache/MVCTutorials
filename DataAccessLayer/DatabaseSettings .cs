using System.Data.Entity;
using DataAccessLayer;
using MVC.DataAccessLayer;

namespace DataAccessLayer
{
    public class DatabaseSettings
    {
        public static void SetDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SalesDbContext>());
        }
    }
}