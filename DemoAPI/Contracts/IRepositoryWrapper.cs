namespace DemoAPI.Contracts
{
    public interface IRepositoryWrapper
    {
        IBranchRepo Branch { get; }
        IElectiveRepo Elective { get; }
        IStudentRepo Student { get; }
        void Save();
    }
}
