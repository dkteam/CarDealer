namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCarNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Odo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Odo", c => c.Int(nullable: false));
        }
    }
}
