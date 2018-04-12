namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postDbViewCount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "Tags");
            DropColumn("dbo.Posts", "Tags");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String());
            AddColumn("dbo.Cars", "Tags", c => c.String());
        }
    }
}
