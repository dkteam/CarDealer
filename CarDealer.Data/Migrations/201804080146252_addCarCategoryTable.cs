namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCarCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(maxLength: 256),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Cars", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "CategoryID");
            AddForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory");
            DropIndex("dbo.Cars", new[] { "CategoryID" });
            DropColumn("dbo.Cars", "CategoryID");
            DropTable("dbo.CarCategory");
        }
    }
}
