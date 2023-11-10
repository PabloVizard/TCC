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
    public class ProjetosApp : BaseApp<Projetos, ProjetosModel>, IProjetosApp
    {
        protected readonly IProjetosService _projetosService;
        protected readonly IMapper _mapper;

        public ProjetosApp(IProjetosService projetosService, IMapper mapper) : base(projetosService, mapper)
        {
            _projetosService = projetosService;
            _mapper = mapper;
        }
    }
}
