using SchoolManagement.Models;

namespace SchoolManagement.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
    }
}