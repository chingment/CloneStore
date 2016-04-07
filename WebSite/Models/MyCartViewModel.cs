using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class MyCartViewModel
    {
        public MyCartViewModel()
        {
            this.Pants = new List<Product>();
            this.Shoes = new List<Product>();
            this.Clothes = new List<Product>();
        }


        public List<Product> Clothes { get; set; }

        public List<Product> Shoes { get; set; }

        public List<Product> Pants { get; set; }
    }
}