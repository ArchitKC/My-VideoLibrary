namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStockTableCheck : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StockDetails");
        }
        
        public override void Down()
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
    }
}
