using DemoAPI.Models;

namespace DemoAPI.Contracts
{
    public interface IElectiveRepo
    {
        Task<Elective> GetElective(int electiveId);
        Task<IEnumerable<Elective>> GetAllElectives();
        Task<bool> CreateElective(Elective elective);
        Task<bool> UpdateElective(Elective elective);
        Task<bool> DeleteElective(Elective elective);
        bool Save();
    }
}
