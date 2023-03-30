using Application.Applications.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Services;
using Domain.Services.Interfaces;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class PreRegistroApp: BaseApp<PreRegistro, PreRegistroModel>, IPreRegistroApp
    {
        protected readonly IPreRegistroService _preRegistroService;
        protected readonly IMapper _mapper;

        public PreRegistroApp(IPreRegistroService preRegistroService, IMapper mapper) : base(preRegistroService, mapper)
        {
            _preRegistroService = preRegistroService;
            _mapper = mapper;

        }
    }
    
}
