using AutoMapper;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TarefaAlunoService : BaseService<TarefaAluno>, ITarefaAlunoService
    {
        private readonly ITarefaAlunoRepository _tarefaAlunoRepository;
        private readonly IMapper _mapper;
        public TarefaAlunoService(ITarefaAlunoRepository tarefaAlunoRepository, IMapper mapper) : base(tarefaAlunoRepository, mapper)
        {
            _tarefaAlunoRepository = tarefaAlunoRepository;
            _mapper = mapper;
        }
    }
}
