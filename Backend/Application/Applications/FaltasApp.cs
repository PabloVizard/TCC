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
    public class FaltasApp : BaseApp<Faltas, FaltasModel>, IFaltasApp
    {
        protected readonly IFaltasService _faltasService;
        protected readonly IMapper _mapper;

        public FaltasApp(IFaltasService faltasService, IMapper mapper) : base(faltasService, mapper)
        {
            _faltasService = faltasService;
            _mapper = mapper;
        }
    }
}
