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
    public class UsuarioTurmaApp : BaseApp<UsuarioTurma, UsuarioTurmaModel>, IUsuarioTurmaApp
    {
        protected readonly IUsuarioTurmaService _usuarioTurmaService;
        protected readonly IMapper _mapper;

        public UsuarioTurmaApp(IUsuarioTurmaService usuarioTurmaService, IMapper mapper) : base(usuarioTurmaService, mapper)
        {
            _usuarioTurmaService = usuarioTurmaService;
            _mapper = mapper;
        }
    }
}
