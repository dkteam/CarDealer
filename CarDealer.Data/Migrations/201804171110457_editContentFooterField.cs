namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editContentFooterField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Footers", "Content", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Footers", "Content", c => c.String(nullable: false));
        }
    }
}
