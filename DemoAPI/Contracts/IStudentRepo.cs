using DemoAPI.Models;

namespace DemoAPI.Contracts;

public interface IStudentRepo
{
    Task<IEnumerable<Student>> GetStudents();
    Task<bool> CreateStudent(Student student);
    Task<bool> CreateStudents(IEnumerable<Student> students);
    Task<bool> UpdateStudent(Student student);
    Task<bool> DeleteStudent(Student student);
    Task<IEnumerable<Student>> GetStudentByStudentId(int studentId);
    bool Save();
}
