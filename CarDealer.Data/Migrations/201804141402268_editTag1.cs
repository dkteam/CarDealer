namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTag1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tags", "Alias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Alias", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
