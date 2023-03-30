
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> AsNoTracking();

        T Find(int id);
        Task<T> FindAsync(int id);

        T FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindByAsync(Expression<Func<T, bool>> predicate);

        List<T> List();
        Task<List<T>> ListAsync();

        List<T> List(Expression<Func<T, bool>> predicate);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query();

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
