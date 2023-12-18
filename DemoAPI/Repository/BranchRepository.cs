using Microsoft.EntityFrameworkCore;
using DemoAPI.Context;
using DemoAPI.Contracts;
using DemoAPI.Models;

namespace DemoAPI.Repository;

public class BranchRepository : StudentBase<Branch>, IBranchRepo
{
    private readonly DataContext dbContext;
    public BranchRepository(DataContext repositoryContext) : base(repositoryContext)
    {
        dbContext = repositoryContext;
    }

    public override bool Create(Branch branch)
    {
        dbContext.Add(branch);
        return Save();
    }

    public async Task<bool> CreateBranch(Branch branch)
    {
        dbContext.Add(branch);
        return await Task.FromResult(Save());
    }

    public override bool Delete(Branch branch)
    {
        dbContext.Remove(branch);
        return Save();
    }

    public async Task<bool> DeleteBranch(int branchID)
    {
        var branchToDelete = await GetBranch(branchID);
        if (branchToDelete is not null)
        {
            dbContext.Branches.Remove(branchToDelete);
            return Save();
        }
        return false;
    }

    public async Task<Branch> GetBranch(int branchId)
    {
        return await dbContext.Branches.Where(b => b.BranchId == branchId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Branch> > GetBranches()
    {
        return await Task.FromResult(FindAll().ToList());
    }

    public bool Save()
    {
        return dbContext.SaveChanges() > 0;
    }

    public override bool Update(Branch branch)
    {
        dbContext.Update(branch);
        return Save();
    }

    public async Task<bool> UpdateBranch(Branch branch)
    {
        var entity = dbContext.Branches.Where(item => item.BranchId == branch.BranchId)
            .FirstOrDefault();

        if (entity is not null)
        {
            entity.BranchName = branch.BranchName;
            return await Task.FromResult(Save());
        }
        else return false;
    }
}