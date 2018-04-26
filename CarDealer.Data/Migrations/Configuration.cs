namespace CarDealer.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealer.Data.CarDealerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CarDealer.Data.CarDealerDbContext context)
        {
            CreateCarCategorySample(context);
            CreateMenuGroupSample(context);
            CreatePageSample(context);
            //CreateConfigTitle(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //  This method will be called after migrating to the latest version.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CarDealerDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CarDealerDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "mr.khoa",
            //    Email = "mr.pdkhoa@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Technology Education"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("mr.pdkhoa@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });


        }

        private void CreatePageSample(CarDealer.Data.CarDealerDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = "Nội dung giới thiệu",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        //private void CreateConfigTitle(CarDealer.Data.CarDealerDbContext context)
        //{
        //    if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
        //    {
        //        context.SystemConfigs.Add(new SystemConfig()
        //        {
        //            Code = "HomeTitle",
        //            ValueString="Trang chủ phamtan-mercedes.com - chuyên trang kinh doanh về thương hiệu xe Mercedes-Benz"
        //        });
        //    }
        //    if (!context.SystemConfigs.Any(x => x.Code == "HomeMetakeyword"))
        //    {
        //        context.SystemConfigs.Add(new SystemConfig()
        //        {
        //            Code = "HomeMetakeyword",
        //            ValueString = "Chuyên trang kinh doanh về thương hiệu xe Mercedes-Benz"
        //        });
        //    }
        //    if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
        //    {
        //        context.SystemConfigs.Add(new SystemConfig()
        //        {
        //            Code = "HomeMetaDescription",
        //            ValueString = "Chuyên trang kinh doanh về thương hiệu xe Mercedes-Benz"
        //        });
        //    }
        //}

        private void CreateCarCategorySample(CarDealer.Data.CarDealerDbContext context)
        {
            if (context.CarCategories.Count() == 0)
            {
                List<CarCategory> listCarCategory = new List<CarCategory>()
                {
                    new CarCategory() {Name = "A - Class", Alias="a-class", Status=true },
                    new CarCategory() {Name = "CLA", Alias="cla", Status=true },
                    new CarCategory() {Name = "GLA", Alias="gla", Status=true },
                    new CarCategory() {Name = "GLC", Alias="glc", Status=true },
                    new CarCategory() {Name = "GLE", Alias="gle", Status=true }
                };

                context.CarCategories.AddRange(listCarCategory);
                context.SaveChanges();
            }
        }

        private void CreateMenuGroupSample(CarDealer.Data.CarDealerDbContext context)
        {
            if (context.MenuGroups.Count() == 0)
            {
                List<MenuGroup> listMenuGroup = new List<MenuGroup>()
                {
                    new MenuGroup() {Name = "Top(Đỉnh)"},
                    new MenuGroup() {Name = "Header(Đầu)"},
                    new MenuGroup() {Name = "Sidebar(Cạnh bên)"},
                    new MenuGroup() {Name = "bottom(Đáy)"}
                };

                context.MenuGroups.AddRange(listMenuGroup);
                context.SaveChanges();
            }
        }
    }
}
