namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.PostCategories", "ParentID", c => c.Int());
            AlterColumn("dbo.PostCategories", "DisplayOrder", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostCategories", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.PostCategories", "ParentID", c => c.Int(nullable: false));
            DropTable("dbo.Errors");
        }
    }
}
