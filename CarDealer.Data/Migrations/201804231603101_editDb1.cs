namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TotalSeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "TotalSeatId", c => c.Int());
            CreateIndex("dbo.Cars", "TotalSeatId");
            AddForeignKey("dbo.Cars", "TotalSeatId", "dbo.TotalSeats", "Id");
            DropColumn("dbo.Cars", "TotalSeat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "TotalSeat", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cars", "TotalSeatId", "dbo.TotalSeats");
            DropIndex("dbo.Cars", new[] { "TotalSeatId" });
            DropColumn("dbo.Cars", "TotalSeatId");
            DropTable("dbo.TotalSeats");
        }
    }
}
