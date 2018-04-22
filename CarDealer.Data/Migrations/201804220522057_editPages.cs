namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Alias", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "Alias");
        }
    }
}
