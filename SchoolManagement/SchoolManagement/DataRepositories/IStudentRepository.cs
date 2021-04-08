using System.Collections.Generic;
using SchoolManagement.Models;

namespace SchoolManagement.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);

        IEnumerable<Student> GetAllStudents();
        Student Insert(Student student);

        Student Update(Student updateStudent);

        Student Delete(int id);
    }
}