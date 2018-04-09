namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editCarCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarCategory", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.CarCategory", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.CarCategory", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.CarCategory", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.CarCategory", "MetaKeyword", c => c.String(maxLength: 256));
            AddColumn("dbo.CarCategory", "MetaDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.CarCategory", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarCategory", "Status");
            DropColumn("dbo.CarCategory", "MetaDescription");
            DropColumn("dbo.CarCategory", "MetaKeyword");
            DropColumn("dbo.CarCategory", "UpdatedBy");
            DropColumn("dbo.CarCategory", "UpdatedDate");
            DropColumn("dbo.CarCategory", "CreatedBy");
            DropColumn("dbo.CarCategory", "CreatedDate");
        }
    }
}
