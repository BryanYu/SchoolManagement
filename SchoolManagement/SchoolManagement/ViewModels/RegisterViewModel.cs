using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.CustomerMiddleWares.Util;

namespace SchoolManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        [ValidEmailDomain("gmail.com", ErrorMessage = "電子郵件後綴必須是gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }

        
        public string City { get; set; }
    }
}
