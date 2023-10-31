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
    public class OrientacoesApp : BaseApp<Orientacoes, OrientacoesModel>, IOrientacoesApp
    {
        protected readonly IOrientacoesService _orientacoesService;
        protected readonly IMapper _mapper;

        public OrientacoesApp(IOrientacoesService orientacoesService, IMapper mapper) : base(orientacoesService, mapper)
        {
            _orientacoesService = orientacoesService;
            _mapper = mapper;
        }
    }
}
