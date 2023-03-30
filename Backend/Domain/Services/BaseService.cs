using AutoMapper;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        protected BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _repository.Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.AnyAsync(predicate);
        }

        public virtual T Find(int id)
        {
            return _repository.Find(id);
        }

        public virtual async Task<T> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindByAsync(predicate);
        }

        public virtual List<T> List()
        {
            return _repository.List();
        }

        public virtual async Task<List<T>> ListAsync()
        {
            return await _repository.ListAsync();
        }

        public virtual List<T> List(Expression<Func<T, bool>> predicate)
        {
            return _repository.List(predicate);
        }

        public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.ListAsync(predicate);
        }

        public virtual IQueryable<T> Query()
        {
            return _repository.Query();
        }

        public virtual void Add(T entity)
        {
            _repository.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _repository.AddRange(entities);
        }

        public virtual void Remove(T entity)
        {
            _repository.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}

