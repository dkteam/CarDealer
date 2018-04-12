using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class CarTagViewModel
    {
        public int CarID { set; get; }
        
        public string TagID { set; get; }
        
        public virtual CarViewModel Car { set; get; }
        
        public virtual TagViewModel Tag { set; get; }
    }
}