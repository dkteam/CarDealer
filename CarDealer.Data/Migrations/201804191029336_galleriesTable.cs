namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class galleriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gallerys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Gallerys");
        }
    }
}
