using System.Collections.Generic;
using SchoolManagement.Models;

namespace SchoolManagement.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);

        IEnumerable<Student> GetAllStudents();
        Student Add(Student student);
    }
}