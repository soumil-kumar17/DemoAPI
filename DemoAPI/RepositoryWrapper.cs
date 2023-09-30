using DemoAPI.Context;
using DemoAPI.Contracts;
using DemoAPI.Repository;

namespace DemoAPI
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext _context;
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
                if (_elective == null)
                {
                    _elective = new ElectiveRepository(_context);
                }
                return _elective;
            }
        }
        public IStudentRepo Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_context);
                }
                return _student;
            }
        }
        public IBranchRepo Branch
        {
            get
            {
                if (_branch == null)
                {
                    _branch = new BranchRepository(_context);
                }
                return _branch;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
