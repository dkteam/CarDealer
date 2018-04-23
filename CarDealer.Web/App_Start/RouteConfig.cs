using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealer.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
           // routes.MapRoute(
           //    name: "About",
           //    url: "gioi-thieu.html",
           //    defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
           //    namespaces: new string[] { "CarDealer.Web.Controllers" }
           //);

            routes.MapRoute(
              name: "Menu",
              url: "trang/{alias}.html",
              defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
              namespaces: new string[] { "CarDealer.Web.Controllers" }
          );

            routes.MapRoute(
               name: "Post",
               url: "{alias}.p-{id}.html",
               defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
               namespaces: new string[] { "CarDealer.Web.Controllers" }
           );

            routes.MapRoute(
               name: "Post Category",
               url: "{alias}.pc-{id}.html",
               defaults: new { controller = "Post", action = "Category", id = UrlParameter.Optional },
               namespaces: new string[] { "CarDealer.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Car Category",
                url: "{alias}.cc-{id}.html",
                defaults: new { controller = "Car", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "CarDealer.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Car",
               url: "{alias}.c-{id}.html",
               defaults: new { controller = "Car", action = "Detail", id = UrlParameter.Optional },
               namespaces: new string[] { "CarDealer.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}