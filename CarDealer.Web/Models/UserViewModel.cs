using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class UserViewModel
    {
        public string Id { set; get; }

        public string FullName { set; get; }

        public string Image { set; get; }

        public string UserName { set; get; }

        public string Email { set; get; }

        public string Address { set; get; }

        public DateTime? BirthDay { set; get; }
    }
}