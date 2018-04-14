namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editPostTag : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PostTags", "CreatedDate");
            DropColumn("dbo.PostTags", "CreatedBy");
            DropColumn("dbo.PostTags", "UpdatedDate");
            DropColumn("dbo.PostTags", "UpdatedBy");
            DropColumn("dbo.PostTags", "MetaKeyword");
            DropColumn("dbo.PostTags", "MetaDescription");
            DropColumn("dbo.PostTags", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostTags", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostTags", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.PostTags", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.PostTags", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.PostTags", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.PostTags", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.PostTags", "CreatedDate", c => c.DateTime());
        }
    }
}
