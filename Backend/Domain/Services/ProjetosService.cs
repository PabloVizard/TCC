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
    public class ProjetosService : BaseService<Projetos>, IProjetosService
    {
        private readonly IProjetosRepository _projetosRepository;
        private readonly IMapper _mapper;
        public ProjetosService(IProjetosRepository projetosRepository, IMapper mapper) : base(projetosRepository, mapper)
        {
            _projetosRepository = projetosRepository;
            _mapper = mapper;
        }
    }

}
