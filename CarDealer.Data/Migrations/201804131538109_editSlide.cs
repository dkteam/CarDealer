namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editSlide : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slides", "Image", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Image", c => c.String(maxLength: 256));
        }
    }
}
