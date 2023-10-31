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
    public class UsuarioTurmaService : BaseService<UsuarioTurma>, IUsuarioTurmaService
    {
        private readonly IUsuarioTurmaRepository _usuarioTurmaRepository;
        private readonly IMapper _mapper;
        public UsuarioTurmaService(IUsuarioTurmaRepository usuarioTurmaRepository, IMapper mapper) : base(usuarioTurmaRepository, mapper)
        {
            _usuarioTurmaRepository = usuarioTurmaRepository;
            _mapper = mapper;
        }
    }
}

