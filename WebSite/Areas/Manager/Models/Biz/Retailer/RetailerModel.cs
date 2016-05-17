using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.Biz.Retailer
{
    public class RetailerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string BannerImg { get; set; }

        public string Description { get; set; }

    }
}