using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Infrastructure;
using SchoolManagement.Models;

namespace SchoolManagement.DataRepositories
{
    public class SQLStudentRepository: IStudentRepository
    {
        private readonly AppDbContext _context;

        public SQLStudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public Student GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students;
        }

        public Student Insert(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Update(Student updateStudent)
        {
            var student = _context.Students.Attach(updateStudent);
            student.State = EntityState.Modified;
            _context.SaveChanges();
            return updateStudent;
        }

        public Student Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
