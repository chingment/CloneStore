using System.ComponentModel.DataAnnotations;

namespace WebSite.Areas.Manager.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Account")]
        public string txt_UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string txt_Password { get; set; }

        [Display(Name = "RememberMe?")]
        public bool ckb_RememberMe { get; set; }
    }
}
