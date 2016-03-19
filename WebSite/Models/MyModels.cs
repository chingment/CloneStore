using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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