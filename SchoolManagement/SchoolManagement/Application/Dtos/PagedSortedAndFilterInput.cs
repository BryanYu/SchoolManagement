using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Dtos
{
    public class PagedSortedAndFilterInput
    {
        public PagedSortedAndFilterInput()
        {
            CurrentPage = 1;
            MaxResultCount = 10;
        }

        [Range(0,1000)]
        public int MaxResultCount { get; set; }

        [Range(0,1000)]
        public int CurrentPage { get; set; }

        public string Sorting { get; set; }

        public string FilterText { get; set; }
    }

    
}
