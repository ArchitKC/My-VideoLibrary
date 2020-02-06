namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockTableCheckNew1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        movieName = c.String(),
                        StockRemaining = c.Int(nullable: false),
                        TotalStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockDetails");
        }
    }
}
