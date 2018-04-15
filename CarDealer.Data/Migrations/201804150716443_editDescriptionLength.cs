namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDescriptionLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.CarCategory", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Cars", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Fuels", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.ManufactureYears", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Styles", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.TransmissionTypes", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.InstallmentPaymentMethods", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Periods", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(maxLength: 1000));
            AlterColumn("dbo.PostCategories", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Posts", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Slides", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Posts", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.PostCategories", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Orders", "CustomerMessage", c => c.String(maxLength: 256));
            AlterColumn("dbo.Periods", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.InstallmentPaymentMethods", "Description", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.TransmissionTypes", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Styles", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.ManufactureYears", "Description", c => c.String());
            AlterColumn("dbo.Fuels", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Cars", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.CarCategory", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Brands", "Description", c => c.String(maxLength: 256));
        }
    }
}
