using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string City { get; set; }

        public List<Claim> Claims { get; set; }

        public List<string> Roles{ get; set; }

        public EditUserViewModel()
        {
            Claims = new List<Claim>();
            Roles = new List<string>();
        }
    }
}
