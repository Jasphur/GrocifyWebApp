namespace GrocifyAppMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoryProductsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Amount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Name = c.String(),
                        BoughtBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistoryProductsModels");
        }
    }
}
