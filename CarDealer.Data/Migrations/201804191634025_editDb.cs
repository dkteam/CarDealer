namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gallerys", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gallerys", "CreatedDate");
        }
    }
}
