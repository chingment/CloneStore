using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.Account
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {
            this.Retailers = new List<Retailer>();
            this.Colors = new List<string>();
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public List <Retailer> Retailers{ get; set; }

        public List<string> Colors { get; set; }

    }
}