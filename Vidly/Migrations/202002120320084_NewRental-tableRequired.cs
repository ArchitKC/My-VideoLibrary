namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewRentaltableRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.NewRentals", new[] { "Movie_Id" });
            AlterColumn("dbo.NewRentals", "Movie_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NewRentals", "Movie_Id");
            AddForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.NewRentals", new[] { "Movie_Id" });
            AlterColumn("dbo.NewRentals", "Movie_Id", c => c.Int());
            CreateIndex("dbo.NewRentals", "Movie_Id");
            AddForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies", "Id");
        }
    }
}
