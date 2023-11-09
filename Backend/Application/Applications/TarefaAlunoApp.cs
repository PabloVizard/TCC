using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class TarefaAlunoApp : BaseApp<TarefaAluno, TarefaAlunoModel>, ITarefaAlunoApp
    {
        protected readonly ITarefaAlunoService _tarefaAlunoService;
        protected readonly IMapper _mapper;

        public TarefaAlunoApp(ITarefaAlunoService tarefaAlunoService, IMapper mapper) : base(tarefaAlunoService, mapper)
        {
            _tarefaAlunoService = tarefaAlunoService;
            _mapper = mapper;
        }
        public virtual void UpdateEntity(TarefaAluno tarefaAluno)
        {
            _tarefaAlunoService.Update(tarefaAluno);
        }
    }
}
