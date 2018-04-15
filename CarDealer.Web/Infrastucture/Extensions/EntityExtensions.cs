using CarDealer.Model.Models;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Infrastucture.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {         
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.Description = postCategoryVm.Description;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {       
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;            
            post.Image = postVm.Image;
            post.Description = postVm.Description;
            post.Content = postVm.Content;            
            post.HomeFlag = postVm.HomeFlag;
            post.HotFlag = postVm.HotFlag;
            post.ViewCount = postVm.ViewCount;
            post.Tags = postVm.Tags;

            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }

        public static void UpdateCar(this Car car, CarViewModel carVm)
        {
            car.ID = carVm.ID;
            car.Name = carVm.Name;
            car.Alias = carVm.Alias;
            car.BrandID = carVm.BrandID;
            car.Image = carVm.Image;
            car.MoreImages = carVm.MoreImages;
            car.CarStatus = carVm.CarStatus;
            car.Odo = carVm.Odo;
            car.FuelID = carVm.FuelID;
            car.StyleID = carVm.StyleID;
            car.ManufactureYearID = carVm.ManufactureYearID;
            car.TransmissionTypeID = carVm.TransmissionTypeID;
            car.Engine = carVm.Engine;
            car.FuelConsumption = carVm.FuelConsumption;
            car.WheelDrive = carVm.WheelDrive;
            car.Price = carVm.Price;
            car.PromotionPrice = carVm.PromotionPrice;
            car.Warranty = carVm.Warranty;
            car.Description = carVm.Description;
            car.Content = carVm.Content;
            car.Bestseller = carVm.Bestseller;
            car.HotFlag = carVm.HotFlag;
            car.BestPrice = carVm.BestPrice;
            car.ViewCount = carVm.ViewCount;
            car.CategoryID = carVm.CategoryID;
            car.Tags = carVm.Tags;

            car.CreatedDate = carVm.CreatedDate;
            car.CreatedBy = carVm.CreatedBy;
            car.UpdatedDate = carVm.UpdatedDate;
            car.UpdatedBy = carVm.UpdatedBy;
            car.MetaKeyword = carVm.MetaKeyword;
            car.MetaDescription = carVm.MetaDescription;
            car.Status = carVm.Status;
        }

        public static void UpdateCarCategory(this CarCategory carCategory, CarCategoryViewModel carCategoryVm)
        {
            carCategory.ID = carCategoryVm.ID;
            carCategory.Name = carCategoryVm.Name;
            carCategory.Alias = carCategoryVm.Alias;
            carCategory.Description = carCategoryVm.Description;
            carCategory.ParentID = carCategoryVm.ParentID;
            carCategory.DisplayOrder = carCategoryVm.DisplayOrder;
            carCategory.Image = carCategoryVm.Image;
            carCategory.HomeFlag = carCategoryVm.HomeFlag;

            carCategory.CreatedDate = carCategoryVm.CreatedDate;
            carCategory.CreatedBy = carCategoryVm.CreatedBy;
            carCategory.UpdatedDate = carCategoryVm.UpdatedDate;
            carCategory.UpdatedBy = carCategoryVm.UpdatedBy;
            carCategory.MetaKeyword = carCategoryVm.MetaKeyword;
            carCategory.MetaDescription = carCategoryVm.MetaDescription;
            carCategory.Status = carCategoryVm.Status;
        }

        public static void UpdateManufactureYear(this ManufactureYear manufactureYear, ManufactureYearViewModel manufactureYearVm)
        {
            manufactureYear.ID = manufactureYearVm.ID;
            manufactureYear.Name = manufactureYearVm.Name;
            manufactureYear.Alias = manufactureYearVm.Alias;
            manufactureYear.Description = manufactureYearVm.Description;

            manufactureYear.CreatedDate = manufactureYearVm.CreatedDate;
            manufactureYear.CreatedBy = manufactureYearVm.CreatedBy;
            manufactureYear.UpdatedDate = manufactureYearVm.UpdatedDate;
            manufactureYear.UpdatedBy = manufactureYearVm.UpdatedBy;
            manufactureYear.MetaKeyword = manufactureYearVm.MetaKeyword;
            manufactureYear.MetaDescription = manufactureYearVm.MetaDescription;
            manufactureYear.Status = manufactureYearVm.Status;
        }

        public static void UpdateFuel(this Fuel fuel, FuelViewModel fuelVm)
        {
            fuel.ID = fuelVm.ID;
            fuel.Name = fuelVm.Name;
            fuel.Alias = fuelVm.Alias;
            fuel.Description = fuelVm.Description;
        }

        public static void UpdateStyle(this Style style, StyleViewModel styleVm)
        {
            style.ID = styleVm.ID;
            style.Name = styleVm.Name;
            style.Alias = styleVm.Alias;
            style.Description = styleVm.Description;

            style.CreatedDate = styleVm.CreatedDate;
            style.CreatedBy = styleVm.CreatedBy;
            style.UpdatedDate = styleVm.UpdatedDate;
            style.UpdatedBy = styleVm.UpdatedBy;
            style.MetaKeyword = styleVm.MetaKeyword;
            style.MetaDescription = styleVm.MetaDescription;
            style.Status = styleVm.Status;
        }


        public static void UpdateTransmissionType(this TransmissionType transmissionType, TransmissionTypeViewModel transmissionTypeVm)
        {
            transmissionType.ID = transmissionTypeVm.ID;
            transmissionType.Name = transmissionTypeVm.Name;
            transmissionType.Alias = transmissionTypeVm.Alias;
            transmissionType.Description = transmissionTypeVm.Description;

            transmissionType.CreatedDate = transmissionTypeVm.CreatedDate;
            transmissionType.CreatedBy = transmissionTypeVm.CreatedBy;
            transmissionType.UpdatedDate = transmissionTypeVm.UpdatedDate;
            transmissionType.UpdatedBy = transmissionTypeVm.UpdatedBy;
            transmissionType.MetaKeyword = transmissionTypeVm.MetaKeyword;
            transmissionType.MetaDescription = transmissionTypeVm.MetaDescription;
            transmissionType.Status = transmissionTypeVm.Status;
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.ID = slideVm.ID;
            slide.Name = slideVm.Name;
            slide.Description = slideVm.Description;
            slide.Image = slideVm.Image;
            slide.URL = slideVm.URL;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.Status = slideVm.Status;
        }

        public static void UpdateMenu(this Menu menu, MenuViewModel menuVm)
        {
            menu.ID = menuVm.ID;
            menu.Name = menuVm.Name;
            menu.URL = menuVm.URL;
            menu.DisplayOrder = menuVm.DisplayOrder;
            menu.GroupID = menuVm.GroupID;
            menu.Target = menuVm.Target;            
            menu.Status = menuVm.Status;
        }
    }
}