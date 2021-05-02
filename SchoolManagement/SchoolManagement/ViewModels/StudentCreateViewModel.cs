using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Models;

namespace SchoolManagement.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入姓名"), MaxLength(50, ErrorMessage = "名字不能超過50個字")]
        public string Name { get; set; }

        [Display(Name = "主修科目")]
        [Required]
        public MajorEnum? Major { get; set; }

        [Display(Name = "電子郵件")]
        [Required(ErrorMessage = "請輸入電子郵件")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "電子郵件格式不正確")]
        public string Email { get; set; }

        [Display(Name = "頭像")]
        public List<IFormFile> Photos { get; set; }

        [Display(Name = "入學時間")]
        public DateTime EnrollmentDate { get; set; }
    }
}
