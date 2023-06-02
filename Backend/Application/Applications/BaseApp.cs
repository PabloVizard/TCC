using Application.Applications.Interfaces;
using AutoMapper;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class BaseApp<T, TM> : IBaseApp<T, TM> where T : class where TM : class
    {
        private readonly IBaseService<T> _baseService;
        private readonly IMapper _mapper;

        public BaseApp(IBaseService<T> baseService, IMapper mapper)
        {
            _baseService = baseService;
            _mapper = mapper;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _baseService.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _baseService.AnyAsync(predicate);
        }

        public T Find(int id)
        {
            return _baseService.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _baseService.FindAsync(id);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _baseService.FindBy(predicate);
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _baseService.FindByAsync(predicate);
        }

        public List<T> List()
        {
            return _baseService.List();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _baseService.ListAsync();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            return _baseService.List(predicate);
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _baseService.ListAsync(predicate);
        }

        public IQueryable<T> Query()
        {
            return _baseService.Query();
        }

        public void Add(TM entity)
        {
            _baseService.Add(_mapper.Map<T>(entity));
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _baseService.AddRange(entities);
        }

        public void Remove(T entity)
        {
            _baseService.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _baseService.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _baseService.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _baseService.SaveChangesAsync();
        }
    }
}
