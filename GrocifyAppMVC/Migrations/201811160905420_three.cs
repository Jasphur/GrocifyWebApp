namespace GrocifyAppMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "BoughtBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "BoughtBy");
        }
    }
}
