using CarDealer.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Data
{
    public class CarDealerDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarDealerDbContext() : base("CarDealerConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Brand> Brands { set; get; }
        public DbSet<Car> Cars { set; get; }
        public DbSet<CarTag> CarTags { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Fuel> Fuels { set; get; }
        public DbSet<InstallmentPaymentMethod> InstallmentPaymentMethods { set; get; }        
        public DbSet<InstallmentPeriod> InstallmentPeriods { set; get; }
        public DbSet<ManufactureYear> ManufactureYears { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Period> Periods { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Style> Styles { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<TransmissionType> TransmissionTypes { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<CarCategory> CarCategories { set; get; }
        public DbSet<LandingPage> LandingPages { set; get; }

        public static CarDealerDbContext Create()
        {
            return new CarDealerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId , i.RoleId});
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}
