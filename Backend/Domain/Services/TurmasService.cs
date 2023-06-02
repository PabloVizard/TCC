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
    public class TurmasService : BaseService<Turmas>, ITurmasService
    {
        private readonly ITurmasRepository _turmasRepository;
        private readonly IMapper _mapper;
        public TurmasService(ITurmasRepository turmasRepository, IMapper mapper) : base(turmasRepository, mapper)
        {
            _turmasRepository = turmasRepository;
            _mapper = mapper;
        }
    }
}
