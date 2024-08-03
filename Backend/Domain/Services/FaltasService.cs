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
    public class FaltasService : BaseService<Faltas>, IFaltasService
    {
        private readonly IFaltasRepository _faltasRepository;
        private readonly IMapper _mapper;
        public FaltasService(IFaltasRepository faltasRepository, IMapper mapper) : base(faltasRepository, mapper)
        {
            _faltasRepository = faltasRepository;
            _mapper = mapper;
        }
    }
}
