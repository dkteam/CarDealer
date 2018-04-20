namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLandingPagesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandingPages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Content = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Gallerys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Gallerys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        Image = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.LandingPages");
        }
    }
}
