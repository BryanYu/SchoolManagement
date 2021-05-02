using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolManagement.Infrastructure.Repositories;
using SchoolManagement.Models;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Application
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student, int> _studentRepository;
        public StudentService(IRepository<Student, int> studentRepository)
        {
            this._studentRepository = studentRepository;
        }
        public async Task<List<Student>> GetPaginatedResult(int currentPage, string searchString,string sortBy, int pageSize = 2)
        {
            var query = _studentRepository.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(item => item.Name.Contains(searchString) || item.Email.Contains(searchString));
            }
            query = query.OrderBy(sortBy);
            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
    }
}
