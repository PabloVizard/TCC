using Application.Applications.Interfaces;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class BaseApp<T> : IBaseApp<T> where T : class
    {
        private readonly IBaseService<T> _baseService;

        public BaseApp(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        public void Adicionar(T entity)
        {
            _baseService.Adicionar(entity);
        }

        public void Atualizar(T entity)
        {
            _baseService.Atualizar(entity);
        }

        public async Task<T> BuscarComPesquisa(Expression<Func<T, bool>> lambda)
        {
            return await _baseService.BuscarComPesquisa(lambda);
        }

        public T BuscarPorId(int id)
        {
            return _baseService.BuscarPorId(id);
        }

        public void Excluir(int id)
        {
            _baseService.Excluir(id);
        }

        public async Task<List<T>> Listar()
        {
            return await _baseService.Listar();
        }
    }
}
