using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Models;

namespace SchoolManagement.ViewModels
{
    public class UserClaimsViewModel
    {
        public string UserId { get; set; }

        public List<UserClaim> Claims { get; set; }

        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }
    }
}
