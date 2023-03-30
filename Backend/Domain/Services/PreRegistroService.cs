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
    public class PreRegistroService: BaseService<PreRegistro>, IPreRegistroService
    {
        private readonly IPreRegistroRepository _preRegistroRepository;
        private readonly IMapper _mapper;
        public PreRegistroService(IPreRegistroRepository preRegistroRepository, IMapper mapper) : base(preRegistroRepository, mapper)
        {
            _preRegistroRepository = preRegistroRepository;
            _mapper = mapper;
        }
    }
}
