using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public enum MajorEnum
    {
        [Display(Name = "未分配")]
        None,
        [Display(Name = "計算機科學")]
        ComputerScience,
        [Display(Name = "電子商務")]
        ElectronicCommerce,
        [Display(Name = "數學")]
        Mathematics
    }
}
