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
    public class CompromissosService : BaseService<Compromissos>, ICompromissosService
    {
        private readonly ICompromissosRepository _compromissosRepository;
        private readonly IMapper _mapper;
        public CompromissosService(ICompromissosRepository compromissosRepository, IMapper mapper) : base(compromissosRepository, mapper)
        {
            _compromissosRepository = compromissosRepository;
            _mapper = mapper;
        }
    }
}
