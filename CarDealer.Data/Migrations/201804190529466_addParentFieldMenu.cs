namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addParentFieldMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "ParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "ParentID");
        }
    }
}
