using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models
{
    public class LogViewModel
    {
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