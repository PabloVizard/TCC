using Application.Applications.Interfaces;
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
    public class UsuariosApp : BaseApp<Usuarios>, IUsuariosApp
    {
        protected readonly IUsuariosService _usuariosService;

        public UsuariosApp(IUsuariosService usuariosService) : base(usuariosService)
        {
            _usuariosService = usuariosService;
        }
    }
}
