using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.Account
{
    public class MyCartModel
    {
        public MyCartModel()
        {
            this.Pants = new List<Lumos.Entity.Product>();
            this.Shoes = new List<Lumos.Entity.Product>();
            this.Clothes = new List<Lumos.Entity.Product>();
            this.Retailers = new List<Retailer>();
        }

        public List<Retailer> Retailers { get; set; }

        public List<Lumos.Entity.Product> Clothes { get; set; }

        public List<Lumos.Entity.Product> Shoes { get; set; }

        public List<Lumos.Entity.Product> Pants { get; set; }
    }
}