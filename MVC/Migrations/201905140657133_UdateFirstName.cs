namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UdateFirstName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employee", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false));
        }
    }
}
