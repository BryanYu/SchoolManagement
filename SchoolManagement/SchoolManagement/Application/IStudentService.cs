using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Models;

namespace SchoolManagement.Application
{
    public interface IStudentService
    {
        Task<List<Student>> GetPaginatedResult(int currentPage, string searchString, string sortBy, int pageSize = 2);
    }
}
