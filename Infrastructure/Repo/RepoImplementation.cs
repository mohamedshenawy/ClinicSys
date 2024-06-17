using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DomainService.Repo
{
    public class RepoImplementation<T> : IRepo<T> where T : class
    {
        private readonly Context _context;

        public DbSet<T> dbSet;
        public RepoImplementation(Context context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }
        public  void Create(T model)
        {
            dbSet.Add(model);
        }

        public void Delete(T model)
        {
            dbSet.Remove(model);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] properties)
        {
            var query = properties.Aggregate(dbSet as IQueryable <T>, (current, property) => current.Include(property));
            return query.ToList();
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> where , params Expression<Func<T, object>>[] properties)
        {
            var query = properties.Aggregate(dbSet.Where(where), (current, property) => current.Include(property));
            return  query.ToList();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
