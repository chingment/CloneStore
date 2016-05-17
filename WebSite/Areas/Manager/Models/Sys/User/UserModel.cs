using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int[] UserRoleIds { get; set; }



    }
}