using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Models;

namespace SchoolManagement.DataRepositories
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _students;

        public MockStudentRepository()
        {
            _students = new List<Student>
            {
                new Student
                    {Id = 1, Name = "張三", Major = "計算機科學", Email = "zhang@52abp.com"},
                new Student
                    {Id = 2, Name = "李四", Major = "物流", Email = "lisi@52abp.com"},
                new Student
                    {Id = 3, Name = "趙六", Major = "電商", Email = "zhaoliu@52abp.com"}
            };
        }

        public Student GetStudent(int id)
        {
            var student = _students.FirstOrDefault(item => item.Id == id);
            return student;
        }
    }
}