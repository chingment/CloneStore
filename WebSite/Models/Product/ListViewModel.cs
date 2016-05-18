using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.Product
{
    public class ListViewModel
    {
        public ListViewModel()
        {
            this.Products = new List<Lumos.Entity.Product>();
            this.CartProducts = new List<Lumos.Entity.Product>();
            this.Retailers = new List<Retailer>();

        }

        public int Retailer { get; set; }

        public List<Retailer> Retailers { get; set; }



        public int OmittedProductsPages { get; set; }


        public List<Lumos.Entity.Product> Products { get; set; }

        public List<Lumos.Entity.Product> CartProducts { get; set; }
    }
}