using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class HeaderViewModel
    {
        public IEnumerable<MenuViewModel> Menus { set; get; }
        public SupportOnlineViewModel SupportOnline { set; get; }
        public IEnumerable<CarCategoryViewModel> Categories { set; get; }
    }
}