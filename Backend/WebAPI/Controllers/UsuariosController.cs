using Application.Applications.Interfaces;
using Application.Models;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : BaseController<Usuarios, UsuariosModel>
    {
        private readonly IUsuariosApp _usuariosApp;
        public UsuariosController(IUsuariosApp usuariosApp) : base(usuariosApp)
        {
            _usuariosApp = usuariosApp;
        }
    }
}
