namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editLandingPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandingPages", "Image", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LandingPages", "Image");
        }
    }
}
