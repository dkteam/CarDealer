namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTargetFieldMenu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Menus", "Target", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Menus", "Target", c => c.String(maxLength: 10));
        }
    }
}
