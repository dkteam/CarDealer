using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class MenuGroupViewModel
    {
        public int ID { set; get; }
        
        public string Name { set; get; }

        public List<MenuViewModel> Menus { set; get; }
    }
}