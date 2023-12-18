using DemoAPI.Context;
using DemoAPI.Contracts;
using DemoAPI.Repository;

namespace DemoAPI; 
public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly DataContext _context;
    private IBranchRepo? _branch;
    private IElectiveRepo? _elective;
    private IStudentRepo? _student;
    public RepositoryWrapper(DataContext context)
    {
        _context = context;
    }

    public IElectiveRepo Elective
    {
        get
        {
            _elective ??= new ElectiveRepository(_context);
            return _elective;
        }
    }
    public IStudentRepo Student
    {
        get
        {
            _student ??= new StudentRepository(_context);
            return _student;
        }
    }
    public IBranchRepo Branch
    {
        get
        {
            _branch ??= new BranchRepository(_context);
            return _branch;
        }
    }
    public void Save()
    {
        _context.SaveChanges();
    }
}
