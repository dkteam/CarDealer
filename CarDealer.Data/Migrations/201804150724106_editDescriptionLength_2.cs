namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDescriptionLength_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Cars", "Alias", c => c.String(nullable: false, maxLength: 256, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Alias", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("dbo.Cars", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
