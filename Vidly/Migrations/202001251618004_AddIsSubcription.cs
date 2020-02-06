namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSubcription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsSubscriberToNewsLetter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsSubscriberToNewsLetter");
        }
    }
}
