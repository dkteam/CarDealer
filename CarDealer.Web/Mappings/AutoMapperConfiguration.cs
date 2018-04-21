using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PostCategory, PostCategoryViewModel>().MaxDepth(2);
                cfg.CreateMap<Post, PostViewModel>().MaxDepth(2);
                cfg.CreateMap<Tag, TagViewModel>().MaxDepth(2);
                cfg.CreateMap<Brand, BrandViewModel>().MaxDepth(2);
                cfg.CreateMap<PostCategory, CarCategoryViewModel>().MaxDepth(2);
                cfg.CreateMap<CarTag, CarTagViewModel>().MaxDepth(2);
                cfg.CreateMap<Car, CarViewModel>().MaxDepth(2);
                cfg.CreateMap<Fuel, FuelViewModel>().MaxDepth(2);
                cfg.CreateMap<InstallmentPaymentMethod, InstallmentPaymentMethodViewModel>().MaxDepth(2);
                cfg.CreateMap<InstallmentPeriod, InstallmentPeriodViewModel>().MaxDepth(2);
                cfg.CreateMap<ManufactureYear, ManufactureYearViewModel>().MaxDepth(2);
                cfg.CreateMap<OrderDetail, OrderDetailViewModel>().MaxDepth(2);
                cfg.CreateMap<Order, OrderViewModel>().MaxDepth(2);
                cfg.CreateMap<Period, PeriodViewModel>().MaxDepth(2);
                cfg.CreateMap<PostTag, PostTagViewModel>().MaxDepth(2);
                cfg.CreateMap<Style, StyleViewModel>().MaxDepth(2);
                cfg.CreateMap<TransmissionType, TransmissionTypeViewModel>().MaxDepth(2);
                cfg.CreateMap<Slide, SlideViewModel>().MaxDepth(2);
                cfg.CreateMap<Menu, MenuViewModel>().MaxDepth(2);
                cfg.CreateMap<MenuGroup, MenuGroupViewModel>().MaxDepth(2);
                cfg.CreateMap<Footer, FooterViewModel>().MaxDepth(2);
                cfg.CreateMap<LandingPage, LandingPageViewModel>().MaxDepth(2);
            });
        }
    }
}