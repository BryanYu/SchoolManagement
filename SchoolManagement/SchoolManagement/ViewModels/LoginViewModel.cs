using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        [Display(Name = "記住我")]
        public bool RememberMe{ get; set; }

        public string ReturnUrl { get; set; }

        public List<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
