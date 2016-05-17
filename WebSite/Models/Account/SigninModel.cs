using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.Account
{
    public class SignInModel
    {
        [Required]
        [Display(Name = "账号")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住密码?")]
        public bool IsRememberMe { get; set; }
    }
}