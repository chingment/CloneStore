using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "账号")]
        public string txt_UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string txt_Password { get; set; }

        [Display(Name = "记住密码?")]
        public bool ckb_RememberMe { get; set; }
    }
}