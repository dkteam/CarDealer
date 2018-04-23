namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editSupportOnline1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupportOnlines", "Image", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupportOnlines", "Image");
        }
    }
}
