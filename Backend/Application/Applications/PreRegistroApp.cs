using Application.Applications.Interfaces;
using Domain.Services;
using Domain.Services.Interfaces;
using Entities.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class PreRegistroApp: BaseApp<PreRegistro>, IPreRegistroApp
    {
        protected readonly IPreRegistroService _preRegistroService;

        public PreRegistroApp(IPreRegistroService preRegistroService) : base(preRegistroService)
        {
            _preRegistroService = preRegistroService;
        }
    }
    
}
