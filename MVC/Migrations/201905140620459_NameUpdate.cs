namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employee", "LastName", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "LastName", c => c.String());
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
