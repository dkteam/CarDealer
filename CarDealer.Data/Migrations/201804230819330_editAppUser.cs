namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editAppUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "Image", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "Image", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
