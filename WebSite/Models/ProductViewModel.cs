using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.Products = new List<Product>();
        }


        public List<Product> Products { get; set; }
    }
}