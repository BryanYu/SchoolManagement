using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.ViewModels
{
    public class StudentEditViewModel : StudentCreateViewModel
    {
        public string Id { get; set; }

        public string ExistingPhotoPath { get; set; }

    }
}
