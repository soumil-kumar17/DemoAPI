using Microsoft.EntityFrameworkCore;
using DemoAPI.Context;
using DemoAPI.Contracts;
using System.Linq.Expressions;

namespace DemoAPI.Repository
{
    public abstract class StudentBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext _context { get; set; }
        public StudentBase(DataContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public IQueryable<T> FindAll() =>_context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _context.Set<T>().Where(expression).AsNoTracking();

        public abstract bool Create(T entity);

        public abstract bool Update(T entity);

        public abstract bool Delete(T entity);
    }
}
