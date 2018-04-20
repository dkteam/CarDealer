using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class PreFooterViewModel
    {
        public IEnumerable<PostViewModel> LatestPosts { set; get; }
        public IEnumerable<CarViewModel> LatestCars { set; get; }
        public SupportOnlineViewModel SupportOnline { set; get; }
    }
}