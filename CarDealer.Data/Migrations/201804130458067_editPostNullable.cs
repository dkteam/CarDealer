namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPostNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            AlterColumn("dbo.Posts", "CategoryID", c => c.Int());
            AlterColumn("dbo.Posts", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "Content", c => c.String());
            CreateIndex("dbo.Posts", "CategoryID");
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Posts", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "CategoryID");
            AddForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories", "ID", cascadeDelete: true);
        }
    }
}
