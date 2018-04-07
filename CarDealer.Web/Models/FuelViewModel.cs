using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class FuelViewModel
    {
        public int ID { set; get; }
        
        public string Name { set; get; }
        
        public string Alias { set; get; }
        
        public string Description { set; get; }

        public virtual IEnumerable<CarViewModel> Cars { set; get; }
    }
}