﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public  interface IBaseService<T> where T : class
    {
        void Adicionar (T entity);
        void Atualizar(T entity);
        void Excluir(int id);
        T BuscarPorId(int id);
        Task<T> BuscarComPesquisa (Expression<Func<T, bool>> lambda);
        Task <List<T>> Listar ();

    }
}
