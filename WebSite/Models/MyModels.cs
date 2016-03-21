using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
namespace WebSite.Models
{


    public class ModesA
    {
        public string UserNameA { get; set; }
    }

    public class MyModels
    {
        public MyModels()
        {
            ModesA = new ModesA();
        }
        public ModesA ModesA { get; set; }

        public string UserName { get; set; }
    }
}