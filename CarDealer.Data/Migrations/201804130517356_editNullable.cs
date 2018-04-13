namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory");
            DropForeignKey("dbo.Cars", "FuelID", "dbo.Fuels");
            DropForeignKey("dbo.Cars", "ManufactureYearID", "dbo.ManufactureYears");
            DropForeignKey("dbo.Cars", "StyleID", "dbo.Styles");
            DropForeignKey("dbo.Cars", "TransmissionTypeID", "dbo.TransmissionTypes");
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups");
            DropIndex("dbo.Cars", new[] { "BrandID" });
            DropIndex("dbo.Cars", new[] { "CategoryID" });
            DropIndex("dbo.Cars", new[] { "FuelID" });
            DropIndex("dbo.Cars", new[] { "StyleID" });
            DropIndex("dbo.Cars", new[] { "ManufactureYearID" });
            DropIndex("dbo.Cars", new[] { "TransmissionTypeID" });
            DropIndex("dbo.Menus", new[] { "GroupID" });
            AlterColumn("dbo.Brands", "Country", c => c.String(maxLength: 50));
            AlterColumn("dbo.Brands", "Logo", c => c.String(maxLength: 256));
            AlterColumn("dbo.Brands", "Website", c => c.String(maxLength: 50));
            AlterColumn("dbo.Cars", "BrandID", c => c.Int());
            AlterColumn("dbo.Cars", "CategoryID", c => c.Int());
            AlterColumn("dbo.Cars", "FuelID", c => c.Int());
            AlterColumn("dbo.Cars", "StyleID", c => c.Int());
            AlterColumn("dbo.Cars", "ManufactureYearID", c => c.Int());
            AlterColumn("dbo.Cars", "TransmissionTypeID", c => c.Int());
            AlterColumn("dbo.Menus", "GroupID", c => c.Int());
            AlterColumn("dbo.Slides", "DisplayOrder", c => c.Int());
            CreateIndex("dbo.Cars", "BrandID");
            CreateIndex("dbo.Cars", "CategoryID");
            CreateIndex("dbo.Cars", "FuelID");
            CreateIndex("dbo.Cars", "StyleID");
            CreateIndex("dbo.Cars", "ManufactureYearID");
            CreateIndex("dbo.Cars", "TransmissionTypeID");
            CreateIndex("dbo.Menus", "GroupID");
            AddForeignKey("dbo.Cars", "BrandID", "dbo.Brands", "ID");
            AddForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory", "ID");
            AddForeignKey("dbo.Cars", "FuelID", "dbo.Fuels", "ID");
            AddForeignKey("dbo.Cars", "ManufactureYearID", "dbo.ManufactureYears", "ID");
            AddForeignKey("dbo.Cars", "StyleID", "dbo.Styles", "ID");
            AddForeignKey("dbo.Cars", "TransmissionTypeID", "dbo.TransmissionTypes", "ID");
            AddForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups");
            DropForeignKey("dbo.Cars", "TransmissionTypeID", "dbo.TransmissionTypes");
            DropForeignKey("dbo.Cars", "StyleID", "dbo.Styles");
            DropForeignKey("dbo.Cars", "ManufactureYearID", "dbo.ManufactureYears");
            DropForeignKey("dbo.Cars", "FuelID", "dbo.Fuels");
            DropForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory");
            DropForeignKey("dbo.Cars", "BrandID", "dbo.Brands");
            DropIndex("dbo.Menus", new[] { "GroupID" });
            DropIndex("dbo.Cars", new[] { "TransmissionTypeID" });
            DropIndex("dbo.Cars", new[] { "ManufactureYearID" });
            DropIndex("dbo.Cars", new[] { "StyleID" });
            DropIndex("dbo.Cars", new[] { "FuelID" });
            DropIndex("dbo.Cars", new[] { "CategoryID" });
            DropIndex("dbo.Cars", new[] { "BrandID" });
            AlterColumn("dbo.Slides", "DisplayOrder", c => c.Int(nullable: false));
            AlterColumn("dbo.Menus", "GroupID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "TransmissionTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "ManufactureYearID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "StyleID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "FuelID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "BrandID", c => c.Int(nullable: false));
            AlterColumn("dbo.Brands", "Website", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Brands", "Logo", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Brands", "Country", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Menus", "GroupID");
            CreateIndex("dbo.Cars", "TransmissionTypeID");
            CreateIndex("dbo.Cars", "ManufactureYearID");
            CreateIndex("dbo.Cars", "StyleID");
            CreateIndex("dbo.Cars", "FuelID");
            CreateIndex("dbo.Cars", "CategoryID");
            CreateIndex("dbo.Cars", "BrandID");
            AddForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "TransmissionTypeID", "dbo.TransmissionTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "StyleID", "dbo.Styles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "ManufactureYearID", "dbo.ManufactureYears", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "FuelID", "dbo.Fuels", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "CategoryID", "dbo.CarCategory", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "BrandID", "dbo.Brands", "ID", cascadeDelete: true);
        }
    }
}
