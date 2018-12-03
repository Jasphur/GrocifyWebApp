namespace GrocifyAppMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HiddenStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoryProductsModels", "HiddenStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "HiddenStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "HiddenStatus");
            DropColumn("dbo.HistoryProductsModels", "HiddenStatus");
        }
    }
}
