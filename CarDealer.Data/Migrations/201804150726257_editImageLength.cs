namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editImageLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Image", c => c.String(nullable: false, maxLength: 1000, unicode: false));
            AlterColumn("dbo.Posts", "Image", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Image", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Cars", "Image", c => c.String(nullable: false, maxLength: 256, unicode: false));
        }
    }
}
