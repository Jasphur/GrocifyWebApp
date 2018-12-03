namespace GrocifyAppMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "BoughtBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "BoughtBy", c => c.String(nullable: false));
        }
    }
}
