using System;
using Lumos.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Retailers = new List<Retailer>();
        }
        public List<Retailer> Retailers { get; set; }
    }
}