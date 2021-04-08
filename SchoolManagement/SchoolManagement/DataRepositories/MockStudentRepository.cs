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
                    {Id = 1, Name = "張三", Major = MajorEnum.ComputerScience, Email = "zhang@52abp.com"},
                new Student
                    {Id = 2, Name = "李四", Major = MajorEnum.ElectronicCommerce, Email = "lisi@52abp.com"},
                new Student
                    {Id = 3, Name = "趙六", Major = MajorEnum.Mathematics, Email = "zhaoliu@52abp.com"}
            };
        }

        public Student GetStudentById(int id)
        {
            var student = _students.FirstOrDefault(item => item.Id == id);
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student Insert(Student student)
        {
            student.Id = _students.Max(item => item.Id) + 1;
            _students.Add(student);
            return student;
        }

        public Student Update(Student updateStudent)
        {
            Student student = _students.FirstOrDefault(item => item.Id == updateStudent.Id);
            if (student != null)
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.Major = updateStudent.Major;
            }

            return student;
        }

        public Student Delete(int id)
        {
            Student student = _students.FirstOrDefault(item => item.Id == id);
            if (student != null)
            {
                _students.Remove(student);
            }

            return student;
        }
    }
}