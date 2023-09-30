using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DemoAPI.Context;
using DemoAPI.Contracts;
using DemoAPI.Models;

namespace DemoAPI.Repository
{
    public class ElectiveRepository : StudentBase<Elective>, IElectiveRepo
    {
        private readonly DataContext dbContext;
        public ElectiveRepository(DataContext context) : base(context)
        {
            dbContext = context;
        }

        public override bool Create(Elective elective)
        {
            dbContext.Add(elective);
            return Save();
        }

        public async Task<bool> CreateElective(Elective elective)
        {
            dbContext.Add(elective);
            return await Task.FromResult(Save());
        }

        public override bool Delete(Elective elective)
        {
            dbContext.Remove(elective);
            return Save();
        }

        public async Task< bool> DeleteElective(Elective elective)
        {
            dbContext.Remove(elective);
            return await Task.FromResult(Save());
        }

        public async Task<IEnumerable<Elective>> GetAllElectives()
        {
            return await Task.FromResult(FindAll().ToList());
        }

        public Task<Elective> GetElective(int electiveID)
        {
            return dbContext.Electives.Where(e => e.ElectiveId == electiveID).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            return dbContext.SaveChanges() > 0;
        }

        public override bool Update(Elective elective)
        {
            dbContext.Update(elective);
            return Save();
        }

        public async Task<bool> UpdateElective(Elective elective)
        {
            var entity = await dbContext.Electives.Where(item => item.ElectiveId == elective.ElectiveId).FirstOrDefaultAsync();

            if(entity != null)
            {
                entity.ElectiveName = elective.ElectiveName;
                return await Task.FromResult(Save());
            }
            else return false;  
        }
    }
}
