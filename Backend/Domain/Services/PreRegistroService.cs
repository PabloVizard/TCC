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
    public class PreRegistroService: BaseService<PreRegistro>, IPreRegistroService
    {
        private readonly IPreRegistroRepository _preRegistroRepository;
        public PreRegistroService(IPreRegistroRepository preRegistroRepository) : base(preRegistroRepository)
        {
            _preRegistroRepository = preRegistroRepository;
        }
    }
}
