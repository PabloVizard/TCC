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
    public class AulasApp : BaseApp<Aulas, AulasModel>, IAulasApp
    {
        protected readonly IAulasService _aulasService;
        protected readonly IMapper _mapper;

        public AulasApp(IAulasService aulasService, IMapper mapper) : base(aulasService, mapper)
        {
            _aulasService = aulasService;
            _mapper = mapper;
        }
    }
}
