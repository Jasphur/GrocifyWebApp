namespace GrocifyAppMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Products", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Name", c => c.String());
            DropColumn("dbo.Products", "Productnaam");
            DropColumn("dbo.Products", "Hoeveelheid");
            DropColumn("dbo.Products", "Naam");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Naam", c => c.String());
            AddColumn("dbo.Products", "Hoeveelheid", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Productnaam", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Products", "Name");
            DropColumn("dbo.Products", "Amount");
            DropColumn("dbo.Products", "ProductName");
        }
    }
}
