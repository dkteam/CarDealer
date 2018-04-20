namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editGalleries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gallerys", "Name", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gallerys", "Name");
        }
    }
}
