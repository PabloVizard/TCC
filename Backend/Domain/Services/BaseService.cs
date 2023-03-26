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
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public void Adicionar(T entity)
        {
             _baseRepository.Add(entity);
        }

        public void Atualizar(T entity)
        {
            _baseRepository.Update(entity);
        }

        public async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> lambda)
        {
            return await _baseRepository.FindByAtribute(lambda);
        }

        public T BuscarPorId(int id)
        {
            return _baseRepository.Find(id);
        }

        public void Excluir(int id)
        {
            _baseRepository.Remove(id);
        }

        public async Task<List<T>> Listar()
        {
            return await _baseRepository.ToListAsync();
        }
    }
}

