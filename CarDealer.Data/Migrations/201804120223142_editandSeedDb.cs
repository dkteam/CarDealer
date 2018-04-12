namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editandSeedDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "ViewCount", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ViewCount", c => c.Int(nullable: false));
        }
    }
}
