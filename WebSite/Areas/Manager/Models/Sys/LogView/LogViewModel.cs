using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.LogView
{
    public class IndexModel
    {
        public IndexModel()
        {
            this.Files=new FileInfo[0];
            this.Dirs = new DirectoryInfo[0];
        }

        public String Parent {
            get;
            set;
        }
        public FileInfo[] Files{
            get;
            set;
        }
        public DirectoryInfo[] Dirs{
            get;
            set;
        }
    }
}