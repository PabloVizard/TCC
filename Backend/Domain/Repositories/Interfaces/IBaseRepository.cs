
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
         bool Any(Func<T, bool> lambda);

         T Find(int id);

         Task<T> FindByAtribute(Expression<Func<T, bool>> lambda);

         List<T> ToList();


         Task<List<T>> ToListAsync();


         Task<List<T>> ToListAsNoTracking();


         IQueryable<T> AsNoTracking();

         void Add(T item);


         void Update(T item);

         void Entry(T item);

         void Remove(int id);

         void Save();

         Task SaveAsync();

    }
}
