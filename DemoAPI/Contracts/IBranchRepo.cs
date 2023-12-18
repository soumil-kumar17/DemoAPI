using DemoAPI.Models;

namespace DemoAPI.Contracts;

public interface IBranchRepo
{
    Task<Branch> GetBranch(int branchId);
    Task<IEnumerable<Branch>> GetBranches();
    Task<bool> CreateBranch(Branch branch);
    Task<bool> UpdateBranch(Branch branch);
    Task<bool> DeleteBranch(int branchID);
    bool Save();
}
