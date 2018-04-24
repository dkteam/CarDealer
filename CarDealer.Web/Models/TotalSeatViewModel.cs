using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class TotalSeatViewModel
    {
        public int Id { set; get; }
        
        public string Name { set; get; }

        public virtual IEnumerable<CarViewModel> Cars { set; get; }
    }
}