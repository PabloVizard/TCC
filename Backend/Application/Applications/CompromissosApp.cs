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
    public class CompromissosApp : BaseApp<Compromissos, CompromissosModel>, ICompromissosApp
    {
        protected readonly ICompromissosService _compromissosService;
        protected readonly IMapper _mapper;

        public CompromissosApp(ICompromissosService compromissosService, IMapper mapper) : base(compromissosService, mapper)
        {
            _compromissosService = compromissosService;
            _mapper = mapper;
        }
    }
}
