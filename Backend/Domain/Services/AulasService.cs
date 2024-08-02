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
    public class AulasService : BaseService<Aulas>, IAulasService
    {
        private readonly IAulasRepository _aulasRepository;
        private readonly IMapper _mapper;
        public AulasService(IAulasRepository aulasRepository, IMapper mapper) : base(aulasRepository, mapper)
        {
            _aulasRepository = aulasRepository;
            _mapper = mapper;
        }
    }
}
