namespace MVC.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FisrtnameUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "FirstName", c => c.String());
        }
    }
}
