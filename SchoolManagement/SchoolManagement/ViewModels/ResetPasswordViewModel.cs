using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "信箱地址:")]
        public string Email{ get; set; }

        [Required]
        [Display(Name = "密碼:")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼:")]
        [Compare("Password", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
