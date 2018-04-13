namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editSupportOnline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupportOnlines", "Address", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupportOnlines", "Address");
        }
    }
}
