namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeatureField2Car : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Features", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Features");
        }
    }
}
