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
        
        public virtual CarViewModel Cars { set; get; }
        
        public virtual TagViewModel Tags { set; get; }
    }
}