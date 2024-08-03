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
    public class TurmasApp : BaseApp<Turmas, TurmasModel>, ITurmasApp
    {
        protected readonly ITurmasService _turmasService;
        protected readonly IMapper _mapper;

        public TurmasApp(ITurmasService turmasService, IMapper mapper) : base(turmasService, mapper)
        {
            _turmasService = turmasService;
            _mapper = mapper;
        }
        public TurmasModel ObterTurmaPorId(int idTurma)
        {
            var turma = _turmasService.Find(idTurma);

            if (turma == null)
            {
                return null;
            }
            var turmaModel = _mapper.Map<TurmasModel>(turma);

            return turmaModel;
        }
    }
}
