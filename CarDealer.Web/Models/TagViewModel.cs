﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class TagViewModel
    {
        public string ID { set; get; }
        
        public string Name { set; get; }
        
        public string Alias { set; get; }
        
        public string Type { set; get; }

        public virtual IEnumerable<CarTagViewModel> CarTags { set; get; }

        public virtual IEnumerable<PostTagViewModel> PostTags { set; get; }
    }
}