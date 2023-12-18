using Microsoft.EntityFrameworkCore;
using DemoAPI.Context;
using DemoAPI.Contracts;
using DemoAPI.Models;

namespace DemoAPI.Repository;

public class StudentRepository : StudentBase<Student>,IStudentRepo
{
    private readonly DataContext dbContext;
    public StudentRepository(DataContext context) : base(context) { dbContext = context; }
    public async Task<bool> CreateStudent(Student student)
    {
        dbContext.Add(student);
        return await Task.FromResult(Save());
    }
    public override bool Create(Student student)
    {
        dbContext.Add(student);
        return Save();
    }

    public async Task<bool> DeleteStudent(Student student)
    {
        dbContext.Remove(student);
        return await Task.FromResult(Save());
    }
    public override bool Update(Student student)
    {
        dbContext.Update(student);
        return Save();
    }

    public async Task<IEnumerable<Student>> GetStudentByStudentId(int studentId)
    {
        //return dbContext.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
        return await Task.FromResult(FindByCondition(s => s.StudentId == studentId).ToList());

    }
    public async Task<IEnumerable<Student>> GetStudents()
    {
        //return dbContext.Students.ToList();
        return await Task.FromResult(FindAll().ToList().OrderBy(i => i.StudentId));
    }

    public bool Save()
    {
        return dbContext.SaveChanges() > 0;
    }

    public async Task<bool> UpdateStudent(Student student)
    {
        var entity = await dbContext.Students.Where(item => item.StudentId == student.StudentId).FirstOrDefaultAsync();
        if (entity != null)
        {
            entity.FirstName = student.FirstName;
            entity.LastName = student.LastName;
            entity.BranchName = student.BranchName;
            entity.Role = student.Role;
            return await Task.FromResult(Save());
        }
        else return false;
    }
    public async Task<IEnumerable<Student>> GetStudentById(int studentId)
    {
        //return dbContext.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
        return await Task.FromResult(FindByCondition(s => s.StudentId == studentId).ToList());
    }

    public override bool Delete(Student student)
    {
        dbContext.Remove(student);
        return Save();
    }

    public async Task<bool> CreateStudents(IEnumerable<Student> students)
    {
        dbContext.AddRange(students);
        return await Task.FromResult(Save());
    }
}
