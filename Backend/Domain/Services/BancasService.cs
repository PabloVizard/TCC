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
    public class BancasService : BaseService<Bancas>, IBancasService
    {
        private readonly IBancasRepository _bancasRepository;
        private readonly IMapper _mapper;
        public BancasService(IBancasRepository bancasRepository, IMapper mapper) : base(bancasRepository, mapper)
        {
            _bancasRepository = bancasRepository;
            _mapper = mapper;
        }
    }
}
