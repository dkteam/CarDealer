namespace CarDealer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carCategoriesTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CarCategory", newName: "CarCategories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CarCategories", newName: "CarCategory");
        }
    }
}
