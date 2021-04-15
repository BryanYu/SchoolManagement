using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            this.Users = new List<string>();
        }
        [Display(Name = "角色Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "角色名稱為必填")]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }

        public List<string> Users{ get; set; }
    }
}
