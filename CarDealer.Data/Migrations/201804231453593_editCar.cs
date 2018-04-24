namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "TotalSeat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "TotalSeat");
        }
    }
}
