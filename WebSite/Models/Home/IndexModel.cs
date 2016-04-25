using System;
using Lumos.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.Home
{
    public class IndexModel
    {
        public IndexModel()
        {
            this.Retailers = new List<Retailer>();
        }
        public List<Retailer> Retailers { get; set; }
    }
}