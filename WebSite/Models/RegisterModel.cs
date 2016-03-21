using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class RegisterModel
    {
        public string txt_UserName { get; set; }

        public string txt_Password { get; set; }

        public string txt_FirstName { get; set; }

        public string txt_LastName { get; set; }

        public string txt_Address { get; set; }

        public string[] cb_Retailers { get; set; }

        public string[] cb_Colors { get; set; }
    }
}