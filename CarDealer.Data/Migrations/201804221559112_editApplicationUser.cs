namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Image", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Image");
        }
    }
}
