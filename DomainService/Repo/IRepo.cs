using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Repo
{
    public interface IRepo <T> where T : class
    {
        void Create(T model);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] properties);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] properties);
        void Update(T model);
        void Delete(T model);
    }
}
