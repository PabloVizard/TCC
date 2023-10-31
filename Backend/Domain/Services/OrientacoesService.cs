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
    public class OrientacoesService : BaseService<Orientacoes>, IOrientacoesService
    {
        private readonly IOrientacoesRepository _orientacoesRepository;
        private readonly IMapper _mapper;
        public OrientacoesService(IOrientacoesRepository orientacoesRepository, IMapper mapper) : base(orientacoesRepository, mapper)
        {
            _orientacoesRepository = orientacoesRepository;
            _mapper = mapper;
        }
    }
}
